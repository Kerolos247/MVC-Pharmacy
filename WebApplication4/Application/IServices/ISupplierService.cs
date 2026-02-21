using WebApplication4.Application.Common.Results;
using WebApplication4.Application.Dto.Supplier;
using WebApplication4.Domain.Models;

namespace WebApplication4.Application.IServices
{
    public interface ISupplierService
    {
        Task<Result<List<Supplier>>> GetAllSuppliersAsync();
        Task<Result<Supplier?>> GetByIdAsync(int id);
        Task<Result<bool>> CreateAsync(RequestCreateSupplier dto);
        Task<Result<bool>> UpdateAsync(int id, UpdateSupplierDto dto);
        Task<Result<bool>> DeleteAsync(int id);
    }
}
