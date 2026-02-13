using WebApplication4.Application.Dto;

namespace WebApplication4.Domain.IRepository
{
    public interface IInventoryRepo
    {
        Task<List<InventoryDto>> GetAllInventoriesAsync();
        Task<InventoryDto?> GetByIdAsync(int id);
        Task<bool> DeleteAsync(int id);
    }
}
