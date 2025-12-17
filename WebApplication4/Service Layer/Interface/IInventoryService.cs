using WebApplication4.Dto;

namespace WebApplication4.Service_Layer.Interface
{
    public interface IInventoryService
    {
        Task<List<InventoryDto>> GetAllInventoriesAsync();
        Task<InventoryDto?> GetByIdAsync(int id);
        Task<bool> DeleteAsync(int id);
    }
}
