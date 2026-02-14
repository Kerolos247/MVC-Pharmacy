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

       
        public async Task<Result<bool>> CreateAsync(RequestCreateSupplier dto)
        {
            return await _supplierRepo.CreateAsync(dto);
        }

       
        public async Task<Result<bool>> UpdateAsync(int id, UpdateSupplierDto dto)
        {
            return await _supplierRepo.UpdateAsync(id, dto);
        }

        
        public async Task<Result<bool>> DeleteAsync(int id)
        {
            return await _supplierRepo.DeleteAsync(id);
        }
    }
}
