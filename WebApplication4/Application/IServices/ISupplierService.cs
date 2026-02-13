using WebApplication4.Application.Dto;
using WebApplication4.Domain.Models;

namespace WebApplication4.Application.IServices
{
    public interface ISupplierService
    {
        Task<List<Supplier>> GetAllSuppliersAsync();
        Task<Supplier?> GetByIdAsync(int id);
        Task<Supplier> CreateAsync(RequestCreateSupplier dto);
        Task<Supplier?> UpdateAsync(int id, UpdateSupplierDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
