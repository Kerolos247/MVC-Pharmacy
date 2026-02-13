using System.Transactions;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Application.Dto;
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
            return await _context.Prescriptions
                .Include(p => p.Patient)
                .Include(p => p.Pharmacist)
                .FirstOrDefaultAsync(p => p.PrescriptionId == id);
        }

        public async Task<List<Prescription>> GetAllPrescriptionsAsync()
        {
            return await _context.Prescriptions
                                 .Include(p => p.Patient)
                                 .Include(p => p.PrescriptionItems)
                                 .ToListAsync();
        }

        public async Task<Prescription> CreateAsync(RequestCreatePrescription dto)
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
            return prescription;
        }

        public async Task<Prescription?> UpdateAsync(int id, UpdatePrescriptionDto dto)
        {
            var prescription = await _context.Prescriptions.FindAsync(id);
            if (prescription == null) return null;

            if (!string.IsNullOrEmpty(dto.Notes)) prescription.Notes = dto.Notes;
            if (dto.Date.HasValue) prescription.Date = dto.Date.Value;
            if (dto.PatientId.HasValue) prescription.PatientId = dto.PatientId.Value;

            await _context.SaveChangesAsync();
            return prescription;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var prescription = await _context.Prescriptions.FindAsync(id);
            if (prescription == null) return false;

            _context.Prescriptions.Remove(prescription);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<ResponseCostDto> PayAsync(int id, IPayment payment)
        {
            await using var transaction =
                await _context.Database.BeginTransactionAsync();

            try
            {
                var prescription = await _context.Prescriptions.FindAsync(id);

                if (prescription == null)
                    throw new Exception("Prescription not found");

                var notesList = prescription.Notes
                    .Split(',')
                    .Select(n => n.Trim())
                    .ToList();

                var medicines = await _context.Medicines
                    .Where(m => notesList.Contains(m.Name))
                    .ToListAsync();

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

                decimal calc = payment.CalculateCost(medicines);

                prescription.Status = PrescriptionStatus.Paid;

                await _context.SaveChangesAsync();


                await transaction.CommitAsync();

                return new ResponseCostDto
                {
                    Cost = calc,
                    operation = true
                };
            }
            catch (Exception ex)
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
