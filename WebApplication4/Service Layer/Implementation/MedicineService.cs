using Microsoft.EntityFrameworkCore;
using WebApplication4.DB;
using WebApplication4.Dto;
using WebApplication4.Models;
using WebApplication4.Service_Layer.Interface;

namespace WebApplication4.Service_Layer.Implementation
{
    public class MedicineService : IMedicineService
    {
        private readonly ApplicationDbContext _context;

        public MedicineService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Medicine?> GetByIdAsync(int id)
        {
           
            return await _context.Medicines
                .Include(m => m.Category)
                .Include(m => m.Supplier)
                .FirstOrDefaultAsync(m => m.MedicineId == id);
        }

        public async Task<List<Medicine>> GetAllMedicinesAsync()
        {
            return await _context.Medicines
                .Include(m => m.Category)
                .Include(m => m.Supplier)
                .ToListAsync();
        }

        public async Task<Medicine> CreateAsync(RequestCreateMedcine dto)
        {
            var newMedicine = new Medicine
            {
                Name = dto.Name,
                Description = dto.Description,
                DosageForm = dto.DosageForm,
                Strength = dto.Strength,
                Price = dto.Price,
                CategoryId = dto.CategoryId,
                SupplierId = dto.SupplierId
            };

            await _context.Medicines.AddAsync(newMedicine);
            await _context.SaveChangesAsync();

            // بعد إضافة الدواء، نضيف سجل Inventory
            var inventory = new Inventory
            {
                MedicineId = newMedicine.MedicineId,
                Quantity = dto.Quantity,
                ExpiryDate = dto.ExpiryDate
            };

            await _context.Inventories.AddAsync(inventory);
            await _context.SaveChangesAsync();

            return newMedicine;
        }


        public async Task<Medicine?> UpdateAsync(int id, UpdateMedcineDto dto)
        {
            var medicine = await _context.Medicines.FindAsync(id);
            if (medicine == null) return null;

            if (!string.IsNullOrEmpty(dto.Name)) medicine.Name = dto.Name;
            if (!string.IsNullOrEmpty(dto.Description)) medicine.Description = dto.Description;
            if (!string.IsNullOrEmpty(dto.DosageForm)) medicine.DosageForm = dto.DosageForm;
            if (!string.IsNullOrEmpty(dto.Strength)) medicine.Strength = dto.Strength;
            if (dto.Price.HasValue) medicine.Price = dto.Price.Value;
            if (dto.CategoryId.HasValue) medicine.CategoryId = dto.CategoryId.Value;
            if (dto.SupplierId.HasValue) medicine.SupplierId = dto.SupplierId.Value;

            await _context.SaveChangesAsync();
            return medicine;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var medicine = await _context.Medicines.FindAsync(id);
            if (medicine == null) return false;

            _context.Medicines.Remove(medicine);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
