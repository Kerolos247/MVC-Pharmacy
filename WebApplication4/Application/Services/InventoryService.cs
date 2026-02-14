using WebApplication4.Application.Dto;
using WebApplication4.Application.IServices;
using WebApplication4.Domain.IRepository;

namespace WebApplication4.Application.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryRepo _inventoryRepo;

        public InventoryService(IInventoryRepo inventoryRepo)
        {
            _inventoryRepo = inventoryRepo;
        }

       
        public async Task<List<InventoryDto>> GetAllInventoriesAsync()
        {
            return await _inventoryRepo.GetAllInventoriesAsync();
        }

       
        public async Task<InventoryDto?> GetByIdAsync(int id)
        {
            return await _inventoryRepo.GetByIdAsync(id);
        }

       
        public async Task<Result<bool>> DeleteAsync(int id)
        {
            return await _inventoryRepo.DeleteAsync(id);
        }
    }
}
