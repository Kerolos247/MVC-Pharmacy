using WebApplication4.Domain.IRepository;
using WebApplication4.Domain.Models;
using WebApplication4.Application.IServices;
using WebApplication4.Application.Common.Results;
using WebApplication4.Application.Dto.Medcine;

namespace WebApplication4.Application.Services
{
    public class MedicineService : IMedicineService
    {
        private readonly IUnitOfWork _uow;

        public MedicineService(IUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
        }

        // Get all medicines
        public async Task<List<Medicine>> GetAllMedicinesAsync()
        {
            return await _uow.Medicines.GetAllAsync();
        }

        // Get medicine by ID
        public async Task<Medicine?> GetByIdAsync(int id)
        {
            return await _uow.Medicines.GetByIdAsync(id);
        }

        // Create new medicine + inventory
        public async Task<Result<bool>> CreateAsync(RequestCreateMedcine dto)
        {
            await _uow.BeginTransactionAsync();

            try
            {
                // Check if medicine exists
                bool exists = await _uow.Medicines.ExistsAsync(dto.Name, dto.DosageForm, dto.Strength);
                if (exists)
                    return Result<bool>.Failure("Medicine already exists");

                // Map DTO → Entity
                var medicine = new Medicine
                {
                    Name = dto.Name,
                    Description = dto.Description,
                    DosageForm = dto.DosageForm,
                    Strength = dto.Strength,
                    Price = dto.Price,
                    CategoryId = dto.CategoryId,
                    SupplierId = dto.SupplierId
                };

                var inventory = new Inventory
                {
                    Medicine = medicine,
                    Quantity = dto.Quantity,
                    ExpiryDate = dto.ExpiryDate
                };

                // Add to repos
                await _uow.Medicines.AddAsync(medicine);
                await _uow.Inventories.AddAsync(inventory);

                // Commit transaction
                await _uow.CommitAsync();

                return Result<bool>.Success(true);
            }
            catch
            {
                await _uow.RollbackAsync();
                return Result<bool>.Failure("Failed to create medicine");
            }
        }

        // Update existing medicine
        public async Task<Result<bool>> UpdateAsync(int id, UpdateMedcineDto dto)
        {
            await _uow.BeginTransactionAsync();

            try
            {
                var medicine = await _uow.Medicines.GetByIdAsync(id);
                if (medicine == null)
                    return Result<bool>.Failure("Medicine not found");

                // Check for duplicate
                bool exists = await _uow.Medicines.ExistsAsync(dto.Name, dto.DosageForm, dto.Strength);
                if (exists && (dto.Name != medicine.Name || dto.DosageForm != medicine.DosageForm || dto.Strength != medicine.Strength))
                    return Result<bool>.Failure("Another medicine with same name, form, and strength exists");

                // Map updates from DTO
                if (!string.IsNullOrEmpty(dto.Name)) medicine.Name = dto.Name;
                if (!string.IsNullOrEmpty(dto.Description)) medicine.Description = dto.Description;
                if (!string.IsNullOrEmpty(dto.DosageForm)) medicine.DosageForm = dto.DosageForm;
                if (!string.IsNullOrEmpty(dto.Strength)) medicine.Strength = dto.Strength;
                if (dto.Price.HasValue) medicine.Price = dto.Price.Value;
                if (dto.CategoryId.HasValue) medicine.CategoryId = dto.CategoryId.Value;
                if (dto.SupplierId.HasValue) medicine.SupplierId = dto.SupplierId.Value;

                await _uow.Medicines.UpdateAsync(medicine);
                await _uow.CommitAsync();

                return Result<bool>.Success(true);
            }
            catch
            {
                await _uow.RollbackAsync();
                return Result<bool>.Failure("Failed to update medicine");
            }
        }

        // Delete medicine
        public async Task<Result<bool>> DeleteAsync(int id)
        {
            await _uow.BeginTransactionAsync();

            try
            {
                var medicine = await _uow.Medicines.GetByIdAsync(id);
                if (medicine == null)
                    return Result<bool>.Failure("Medicine not found");

                // Check if inventory exists
                bool hasInventory = await _uow.Inventories.ExistsByMedicineIdAsync(id);
                if (hasInventory)
                    return Result<bool>.Failure("Cannot delete medicine with inventory");

                await _uow.Medicines.DeleteAsync(medicine);
                await _uow.CommitAsync();

                return Result<bool>.Success(true);
            }
            catch
            {
                await _uow.RollbackAsync();
                return Result<bool>.Failure("Failed to delete medicine");
            }
        }
    }
}
