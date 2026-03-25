using WebApplication4.Domain.Models;
namespace WebApplication4.Domain.IRepository
{
    public interface ISupplierRepo
    {
        Task<Supplier?> GetByIdAsync(int id);
        Task<IEnumerable<Supplier>> GetAllAsync();
        Task AddAsync(Supplier supplier);
        Task UpdateAsync(Supplier supplier);
        Task DeleteAsync(Supplier supplier);
        Task<int> GetSupplierCountAsync();
    }
}
