using WebApplication4.Application.Dto;
using WebApplication4.Domain.IRepository;
using WebApplication4.Domain.Models;
using WebApplication4.Application.IServices;

namespace WebApplication4.Application.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepo _supplierRepo;

        public SupplierService(ISupplierRepo supplierRepo)
        {
            _supplierRepo = supplierRepo;
        }

        
        public async Task<List<Supplier>> GetAllSuppliersAsync()
        {
            return await _supplierRepo.GetAllSuppliersAsync();
        }

       
        public async Task<Supplier?> GetByIdAsync(int id)
        {
            return await _supplierRepo.GetByIdAsync(id);
        }

       
        public async Task<Supplier> CreateAsync(RequestCreateSupplier dto)
        {
            return await _supplierRepo.CreateAsync(dto);
        }

       
        public async Task<Supplier?> UpdateAsync(int id, UpdateSupplierDto dto)
        {
            return await _supplierRepo.UpdateAsync(id, dto);
        }

        
        public async Task<bool> DeleteAsync(int id)
        {
            return await _supplierRepo.DeleteAsync(id);
        }
    }
}
