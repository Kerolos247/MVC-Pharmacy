using Microsoft.EntityFrameworkCore;
using WebApplication4.DB;
using WebApplication4.Dto;
using WebApplication4.Service_Layer.Interface;

namespace WebApplication4.Service_Layer.Implementation
{
    public class InventoryService : IInventoryService
    {
        private readonly ApplicationDbContext _context;

        public InventoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<InventoryDto>> GetAllInventoriesAsync()
        {
            return await _context.Inventories
                .Include(i => i.Medicine)
                .Select(i => new InventoryDto
                {
                    InventoryId = i.InventoryId,
                    Quantity = i.Quantity,
                    ExpiryDate = i.ExpiryDate,
                    MedicineId = i.MedicineId,
                    MedicineName = i.Medicine.Name,
                    DosageForm = i.Medicine.DosageForm,
                    Strength = i.Medicine.Strength
                })
                .ToListAsync();

        }

        public async Task<InventoryDto?> GetByIdAsync(int id)
        {
            var inv = await _context.Inventories
                .Include(i => i.Medicine)
                .FirstOrDefaultAsync(i => i.InventoryId == id);

            if (inv == null) return null;

            return new InventoryDto
            {
                InventoryId = inv.InventoryId,
                Quantity = inv.Quantity,
                ExpiryDate = inv.ExpiryDate,
                MedicineId = inv.MedicineId,
                MedicineName = inv.Medicine.Name,
                DosageForm = inv.Medicine.DosageForm,
                Strength = inv.Medicine.Strength
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var inventory = await _context.Inventories.FindAsync(id);
            if (inventory == null) return false;

            _context.Inventories.Remove(inventory);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
