using Microsoft.EntityFrameworkCore;
using WebApplication4.Application.Common.Results;
using WebApplication4.Application.Dto.Supplier;
using WebApplication4.Application.IServices;
using WebApplication4.Domain.IRepository;
using WebApplication4.Domain.Models;
using WebApplication4.Infrastructure.DB;

namespace WebApplication4.Infrastructure.Repository
{
    public class SupplierRepo : ISupplierRepo
    {
        private readonly ApplicationDbContext _context;
        private readonly IValidationService _validationService;
        public SupplierRepo(ApplicationDbContext context,IValidationService validationService)
        {
            _context = context;
            _validationService = validationService;
        }

        public async Task<Supplier?> GetByIdAsync(int supplierId)
        {
            // Use AsNoTracking for read-only queries to improve performance
            var supplier = await _context.Suppliers.AsNoTracking()
                .Include(s => s.Medicines)
                    .ThenInclude(m => m.Inventory)
                .Include(s => s.Medicines)
                    .ThenInclude(m => m.Category)
                .FirstOrDefaultAsync(s => s.SupplierId == supplierId);

            return supplier;
        }

        public async Task<List<Supplier>> GetAllSuppliersAsync()
        {
            // Use AsNoTracking for read-only queries to improve performance
            return await _context.Suppliers
                .AsNoTracking()
                .Include(s => s.Medicines)
                .ToListAsync();
        }

        public async Task<Result<bool>> CreateAsync(RequestCreateSupplier supplierDto)
        {
            //Check if the phone number already exists using ValidationService
            if (await _validationService.PhoneExistsAsync<Supplier>(
                    supplierDto.Phone,
                    nameof(Supplier.SupplierId)))
            {
                return Result<bool>.Failure("Phone number already exists");
            }

            //Check if the email already exists using ValidationService
            if (await _validationService.EmailExistsAsync<Supplier>(
                    supplierDto.Email,
                    nameof(Supplier.SupplierId)))
            {
                return Result<bool>.Failure("Email already exists");
            }

            var newSupplier = new Supplier
            {
                Name = supplierDto.Name,
                Phone = supplierDto.Phone,
                Email = supplierDto.Email,
                Medicines = new List<Medicine>()
            };

            await _context.Suppliers.AddAsync(newSupplier);
            await _context.SaveChangesAsync();

            return Result<bool>.Success(true);
        }


        public async Task<Result<bool>> UpdateAsync(int supplierId, UpdateSupplierDto supplierDto)
        {
            var supplier = await _context.Suppliers.FindAsync(supplierId);
            if (supplier == null)
                return Result<bool>.Failure("Not Found Supplier");

            //Check if the new phone number already exists for another supplier
            if (await _validationService.PhoneExistsAsync<Supplier>(
                    supplierDto.Phone,
                    nameof(Supplier.SupplierId),
                    supplierId))
            {
                return Result<bool>.Failure("Phone number already exists");
            }

            //Check if the new email already exists for another supplier
            if (await _validationService.EmailExistsAsync<Supplier>(
                    supplierDto.Email,
                    nameof(Supplier.SupplierId),
                    supplierId))
            {
                return Result<bool>.Failure("Email already exists");
            }

            if (!string.IsNullOrEmpty(supplierDto.Name)) supplier.Name = supplierDto.Name;
            if (!string.IsNullOrEmpty(supplierDto.Phone)) supplier.Phone = supplierDto.Phone;
            if (!string.IsNullOrEmpty(supplierDto.Email)) supplier.Email = supplierDto.Email;

            await _context.SaveChangesAsync();
            return Result<bool>.Success(true);
        }


        public async Task<Result<bool>> DeleteAsync(int supplierId)
        {
            var supplier = await _context.Suppliers.FindAsync(supplierId);
            var hasMedicines = await _context.Medicines.AnyAsync(m => m.SupplierId == supplierId);

            if (hasMedicines || supplier == null)
                return Result<bool>.Failure("Cannot delete supplier with associated medicines or supplier not found");

            _context.Suppliers.Remove(supplier);
            await _context.SaveChangesAsync();
            return Result<bool>.Success(true);
        }

       
    }
}
