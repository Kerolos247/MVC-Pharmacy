using WebApplication4.Application.Dto;
using WebApplication4.Domain.Models;

namespace WebApplication4.Domain.IRepository
{
    public interface ISupplierRepo
    {
        Task<Supplier?> GetByIdAsync(int id);
        Task<List<Supplier>> GetAllSuppliersAsync();
        Task<Supplier> CreateAsync(RequestCreateSupplier dto);
        Task<Supplier?> UpdateAsync(int id, UpdateSupplierDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
