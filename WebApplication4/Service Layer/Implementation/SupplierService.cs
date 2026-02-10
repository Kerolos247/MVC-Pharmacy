using Microsoft.EntityFrameworkCore;
using WebApplication4.DB;
using WebApplication4.Dto;
using WebApplication4.Models;
using WebApplication4.Service_Layer.Interface;

namespace WebApplication4.Service_Layer.Implementation
{
    public class SupplierService : ISupplierService
    {
        private readonly ApplicationDbContext _context;

        public SupplierService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Supplier?> GetByIdAsync(int id)
        {

            var supplier = await _context.Suppliers
                .Include(s => s.Medicines)
                    .ThenInclude(m => m.Inventory) // هذا السطر يحل مشكلة med.Inventory
                    .Include(s => s.Medicines)
                    .ThenInclude(m => m.Category)  // هذا السطر يحل مشكلة med.Category
                      .FirstOrDefaultAsync(s => s.SupplierId == id);


            return supplier;
        }

        public async Task<List<Supplier>> GetAllSuppliersAsync()
        {
            return await _context.Suppliers.AsNoTracking()
                .Include(s => s.Medicines)
                .ToListAsync();
        }

        public async Task<Supplier> CreateAsync(RequestCreateSupplier dto)
        {
            var supplier = new Supplier
            {
                Name = dto.Name,
                Phone = dto.Phone ?? string.Empty,
                Email = dto.Email ?? string.Empty,
                Medicines = new List<Medicine>()
            };

            await _context.Suppliers.AddAsync(supplier);
            await _context.SaveChangesAsync();
            return supplier;
        }

        public async Task<Supplier?> UpdateAsync(int id, UpdateSupplierDto dto)
        {
            var supplier = await _context.Suppliers.FindAsync(id);
            if (supplier == null) return null;

            if (!string.IsNullOrEmpty(dto.Name)) supplier.Name = dto.Name;
            if (!string.IsNullOrEmpty(dto.Phone)) supplier.Phone = dto.Phone;
            if (!string.IsNullOrEmpty(dto.Email)) supplier.Email = dto.Email;

            await _context.SaveChangesAsync();
            return supplier;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var supplier = await _context.Suppliers.FindAsync(id);

            var medcine = await _context.Medicines.AnyAsync(m => m.SupplierId == id);

            if(medcine)
                return false;


            if (supplier == null)
                return false;

            _context.Suppliers.Remove(supplier);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
