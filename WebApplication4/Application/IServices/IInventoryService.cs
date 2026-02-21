using WebApplication4.Application.Common.Results;
using WebApplication4.Application.Dto.Inventory;

namespace WebApplication4.Application.IServices
{
    public interface IInventoryService
    {
        Task<Result<List<InventoryDto>>> GetAllInventoriesAsync();
        Task<Result<InventoryDto?>> GetByIdAsync(int id);
        Task<Result<bool>> DeleteAsync(int id);
    }
}
