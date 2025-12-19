using Azure;
using Microsoft.EntityFrameworkCore;
using WebApplication4.DB;
using WebApplication4.Dto;
using WebApplication4.Models;
using WebApplication4.Service_Layer.Interface;

namespace WebApplication4.Service_Layer.Implementation
{
    public class PrescriptionService :IPrescriptionService
    {
        private readonly ApplicationDbContext _context;
 
        public PrescriptionService(ApplicationDbContext context)
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
        public async Task<ResponseCostDto> PayAsync(int id, IPaymentStrategy payment)
        {
            var prescription = await _context.Prescriptions.FindAsync(id);
            if (prescription == null)
                return new ResponseCostDto{ Cost = 0, operation = false };

            var notesList = prescription.Notes.Split(',').Select(n => n.Trim()).ToList();
            var medicines = await _context.Medicines
                                          .Where(m => notesList.Contains(m.Name))
                                          .ToListAsync();

            foreach (var note in notesList)
            {
                var medcine = medicines.FirstOrDefault(m => m.Name == note);
                if (medcine == null)
                    return new ResponseCostDto { Cost = 0, operation = false };

                var inventoryItem = await _context.Inventories.FirstOrDefaultAsync(i => i.MedicineId == medcine.MedicineId);
                if (inventoryItem?.Quantity > 0)
                {
                    inventoryItem.Quantity -= 1;
                }
                else
                {
                    return new ResponseCostDto { Cost = 0, operation = false };
                }
            }
            
            decimal calc = payment.CalculateCost(medicines);
            prescription.Status = PrescriptionStatus.Paid;
            await _context.SaveChangesAsync();
            return new ResponseCostDto { Cost = calc, operation = true };
        }





    }
}
