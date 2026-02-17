using Microsoft.EntityFrameworkCore;
using WebApplication4.Domain.IRepository;
using WebApplication4.Domain.Models;
using WebApplication4.Infrastructure.DB;

namespace WebApplication4.Infrastructure.Repository
{
    public class InventoryRepo : IInventoryRepo
    {
        private readonly ApplicationDbContext _context;

        public InventoryRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Inventory item)
        {
            await _context.Inventories.AddAsync(item);
        }

        public async Task<List<Inventory>> GetAllAsync()
        {
            return await _context.Inventories
                .Include(i => i.Medicine)
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<bool> ExistsByMedicineIdAsync(int Id)
        {
           return await _context.Inventories.AnyAsync(i => i.MedicineId == Id);
        }

        public async Task<Inventory?> GetByIdAsync(int id)
        {
            return await _context.Inventories
                .Include(i => i.Medicine)
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.InventoryId == id);
        }

        public async Task<Inventory?> GetByMedicineIdAsync(int medicineId)
        {
            return await _context.Inventories
                .Include(i => i.Medicine)
                .FirstOrDefaultAsync(i => i.MedicineId == medicineId);
        }

        public Task UpdateAsync(Inventory item)
        {
            _context.Inventories.Update(item);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Inventory item)
        {
            _context.Inventories.Remove(item);
            return Task.CompletedTask;
        }
    }
}
