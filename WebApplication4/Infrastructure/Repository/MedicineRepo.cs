using Microsoft.EntityFrameworkCore;
using WebApplication4.Application.Dto;
using WebApplication4.Domain.IRepository;
using WebApplication4.Domain.Models;
using WebApplication4.Infrastructure.DB;

namespace WebApplication4.Infrastructure.Repository
{
    public class MedicineRepo : IMedicineRepo
    {
        private readonly ApplicationDbContext _context;

        public MedicineRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Medicine?> GetByIdAsync(int id)
        {

            return await _context.Medicines.AsNoTracking()
                .Include(m => m.Category)
                .Include(m => m.Supplier)
                .Include(i => i.Inventory)
                .FirstOrDefaultAsync(m => m.MedicineId == id);
        }

        public async Task<List<Medicine>> GetAllMedicinesAsync()
        {
            return await _context.Medicines.AsNoTracking()
                .Include(c => c.Category)
                .Include(m => m.Supplier)
                .ToListAsync();
        }

        public async Task<Result<bool>> CreateAsync(RequestCreateMedcine dto)
        {
            //Use a transaction to ensure that both the medicine and inventory are created successfully, and to maintain data integrity in case of any errors during the process
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                //Check if a medicine with the same name, dosage form, and strength already exists to prevent duplicates
                bool exists = await _context.Medicines.AnyAsync(m =>
                    m.Name == dto.Name &&
                    m.DosageForm == dto.DosageForm &&
                    m.Strength == dto.Strength);

                if (exists)
                    return Result<bool>.Failure("Medicine with the same name, form, and strength already exists.");

                //Create a new medicine entity based on the provided DTO and add it to the database
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

                //Save Database
                await _context.Medicines.AddAsync(newMedicine);
                await _context.SaveChangesAsync();

                //Create a new inventory record for the newly created medicine with the specified quantity and expiry date, and add it to the database
                var inventory = new Inventory
                {
                    MedicineId = newMedicine.MedicineId, 
                    Quantity = dto.Quantity,
                    ExpiryDate = dto.ExpiryDate
                };

                //Save Database
                await _context.Inventories.AddAsync(inventory);
                await _context.SaveChangesAsync();

                //Commit the transaction to finalize the changes in the database, ensuring that both the medicine and inventory records are created successfully
                await transaction.CommitAsync();
                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return Result<bool>.Failure($"An error occurred while creating the medicine: {ex.Message}");
            }

        }



        public async Task<Result<bool>> UpdateAsync(int id, UpdateMedcineDto dto)
        {
            //Find the medicine by ID 
            var medicine = await _context.Medicines.FindAsync(id);
            if (medicine == null)
                return Result<bool>.Failure("Not Found Medcine");

            //Check if a medicine with the same name, dosage form, and strength already exists, excluding the current medicine being updated, to prevent duplicates
            bool exists = await _context.Medicines.AnyAsync(m =>
                 m.Name == dto.Name &&
                 m.DosageForm == dto.DosageForm &&
                 m.Strength == dto.Strength &&
                 m.MedicineId != id); // exclude current medicine


            if (exists)
                return Result<bool>.Failure("Medicine with the same name, form, and strength already exists.");


            //Update the medicine properties based on the provided DTO, only if the corresponding values in the DTO are not null or empty, allowing for partial updates of the medicine record
            if (!string.IsNullOrEmpty(dto.Name)) medicine.Name = dto.Name;
            if (!string.IsNullOrEmpty(dto.Description)) medicine.Description = dto.Description;
            if (!string.IsNullOrEmpty(dto.DosageForm)) medicine.DosageForm = dto.DosageForm;
            if (!string.IsNullOrEmpty(dto.Strength)) medicine.Strength = dto.Strength;
            if (dto.Price.HasValue) medicine.Price = dto.Price.Value;
            if (dto.CategoryId.HasValue) medicine.CategoryId = dto.CategoryId.Value;
            if (dto.SupplierId.HasValue) medicine.SupplierId = dto.SupplierId.Value;

            await _context.SaveChangesAsync();
            return Result<bool>.Success(true);
        }

        public async Task<Result<bool>> DeleteAsync(int id)
        {
            var medicine = await _context.Medicines.FindAsync(id);

            var Inventory = await _context.Inventories
                .AnyAsync(i => i.MedicineId == id);

            if (Inventory)
                return Result<bool>.Failure("Not Allowed Delete Medcine Has Invetory");

            if (medicine == null)
                return Result<bool>.Failure("Not Found Medcine");

            _context.Medicines.Remove(medicine);
            await _context.SaveChangesAsync();
            return Result<bool>.Success(true);
        }
    }
}
