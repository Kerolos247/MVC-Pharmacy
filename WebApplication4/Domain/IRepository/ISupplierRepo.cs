using WebApplication4.Application.Dto;
using WebApplication4.Domain.Models;

namespace WebApplication4.Domain.IRepository
{
    public interface ISupplierRepo
    {
        Task<Supplier?> GetByIdAsync(int id);
        Task<List<Supplier>> GetAllSuppliersAsync();
        Task<Result<bool>> CreateAsync(RequestCreateSupplier dto);
        Task<Result<bool>> UpdateAsync(int id, UpdateSupplierDto dto);
        Task<Result<bool>> DeleteAsync(int id);
    }
}
