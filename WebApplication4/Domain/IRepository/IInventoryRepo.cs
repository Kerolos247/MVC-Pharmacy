using WebApplication4.Application.Common.Results;
using WebApplication4.Application.Dto.Inventory;
using WebApplication4.Domain.Models;

namespace WebApplication4.Domain.IRepository
{
    public interface IInventoryRepo
    {
        Task<bool> ExistsByMedicineIdAsync(int Id);
        Task AddAsync(Inventory item);
        Task<List<Inventory>> GetAllAsync();
        Task<Inventory?> GetByIdAsync(int id);
        Task<Inventory?> GetByMedicineIdAsync(int medicineId);
        Task UpdateAsync(Inventory item);
        Task DeleteAsync(Inventory item);
    }
}
