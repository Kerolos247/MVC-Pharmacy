using System.Security.Claims;
using System.Transactions;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Application.Common.Results;
using WebApplication4.Application.Dto.Prescription;
using WebApplication4.Application.IServices;
using WebApplication4.Domain.IRepository;
using WebApplication4.Domain.Models;
using WebApplication4.Infrastructure.DB;

namespace WebApplication4.Infrastructure.Repository
{
    public class PrescriptionRepo : IPrescriptionRepo
    {
        private readonly ApplicationDbContext _context;
        public PrescriptionRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Prescription?> GetByIdAsync(int id)
        {
            //Use AsNoTracking to improve performance since we only need to read the prescription and not track changes to it
            return await _context.Prescriptions.AsNoTracking()
                .Include(p => p.Patient)
                .Include(p => p.Pharmacist)
                .FirstOrDefaultAsync(p => p.PrescriptionId == id);
        }

        public async Task<List<Prescription>> GetAllPrescriptionsAsync()
        {
            //Use AsNoTracking to improve performance since we only need to read the prescriptions and not track changes to them
            return await _context.Prescriptions.AsNoTracking()
                                 .Include(p => p.Patient)
                                 .Include(p => p.PrescriptionItems)
                                 .ToListAsync();
        }

        public async Task<Result<bool>> CreateAsync(RequestCreatePrescription dto)
        {
            var prescription = new Prescription
            {
                Date = dto.Date,
                Notes = dto.Notes,
                PatientId = dto.PatientId,
                PharmacistId = dto.PharmacistId
            };

            await _context.Prescriptions.AddAsync(prescription);
            await _context.SaveChangesAsync();
            return Result<bool>.Success(true);
        }

        public async Task<Result<bool>> UpdateAsync(int id, UpdatePrescriptionDto dto, string pharmacistId)
        {
            //Find the prescription by ID
            var prescription = await _context.Prescriptions.FindAsync(id);
            if (prescription == null)
                return Result<bool>.Failure("Prescription not found");

            //Check if the prescription is already paid, if so, we cannot update it
            if (prescription.Status == PrescriptionStatus.Paid)
                return Result<bool>.Failure("Cannot update a paid prescription");

            if (!string.IsNullOrEmpty(dto.Notes))
                prescription.Notes = dto.Notes;

            if (dto.Date.HasValue)
                prescription.Date = dto.Date.Value;

            if (dto.PatientId.HasValue)
                prescription.PatientId = dto.PatientId.Value;

            //Update the pharmacist ID to the one who is making the update
            prescription.PharmacistId = pharmacistId;

            await _context.SaveChangesAsync();

            return Result<bool>.Success(true);
        }

        public async Task<Result<bool>> DeleteAsync(int id)
        {
            var prescription = await _context.Prescriptions.FindAsync(id);
            if (prescription == null)
                return Result<bool>.Failure("No Found Prescription");

            _context.Prescriptions.Remove(prescription);
            await _context.SaveChangesAsync();
            return Result<bool>.Success(true);
        }

        public async Task<ResponseCostDto> PayAsync(int id, IPayment payment)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                //Bring the prescription
                var prescription = await _context.Prescriptions.FindAsync(id);
                if (prescription == null)
                    throw new Exception("Prescription not found");

                //Split the notes into a list of medicine names, trim any whitespace, and convert to a list
                var notesList = prescription.Notes
                    .Split(',')
                    .Select(n => n.Trim())
                    .ToList();

                //Use AsNoTracking to improve performance since we only need to read the medicines and not track changes to them
                var medicines = await _context.Medicines
                    .AsNoTracking()
                    .Where(m => notesList.Contains(m.Name))
                    .ToListAsync();

                //Check if all medicines in the notes are found in the database
                foreach (var note in notesList)
                {
                    var medicine = medicines.FirstOrDefault(m => m.Name == note);
                    if (medicine == null)
                        throw new Exception("Medicine not found");

                   
                    var inventoryItem = await _context.Inventories
                        .FirstOrDefaultAsync(i => i.MedicineId == medicine.MedicineId);

                    if (inventoryItem == null || inventoryItem.Quantity <= 0)
                        throw new Exception("Not enough stock");

                    inventoryItem.Quantity -= 1;
                }

                //Calculate the total cost using the payment strategy
                decimal calc = payment.CalculateCost(medicines);
                prescription.Status = PrescriptionStatus.Paid;

                //Commit the transaction to ensure that all changes are saved atomically
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return new ResponseCostDto
                {
                    Cost = calc,
                    operation = true
                };
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                return new ResponseCostDto
                {
                    Cost = 0,
                    operation = false
                };
            }
        }
    }
}
