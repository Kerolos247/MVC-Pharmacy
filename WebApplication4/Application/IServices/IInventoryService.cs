using WebApplication4.Application.Dto;

namespace WebApplication4.Application.IServices
{
    public interface IInventoryService
    {
        Task<List<InventoryDto>> GetAllInventoriesAsync();
        Task<InventoryDto?> GetByIdAsync(int id);
        Task<bool> DeleteAsync(int id);
    }
}
