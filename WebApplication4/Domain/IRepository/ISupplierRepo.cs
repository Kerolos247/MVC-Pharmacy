using WebApplication4.Application.Common.Results;
using WebApplication4.Application.Dto.Supplier;
using WebApplication4.Domain.Models;

namespace WebApplication4.Domain.IRepository
{
    public interface ISupplierRepo
    {
        Task<Supplier?> GetByIdAsync(int id);
        Task<List<Supplier>> GetAllAsync();
        Task AddAsync(Supplier supplier);
        Task UpdateAsync(Supplier supplier);
        Task DeleteAsync(Supplier supplier);
    }
}
