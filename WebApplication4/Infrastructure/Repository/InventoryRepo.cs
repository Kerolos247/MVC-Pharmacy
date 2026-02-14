using Microsoft.EntityFrameworkCore;
using WebApplication4.Application.Dto;
using WebApplication4.Domain.IRepository;
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

        public async Task<List<InventoryDto>> GetAllInventoriesAsync()
        {
            return await _context.Inventories.AsNoTracking()
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
            var inv = await _context.Inventories.AsNoTracking()
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

        public async Task<Result<bool>> DeleteAsync(int id)
        {
            var inventory = await _context.Inventories.FindAsync(id);
            if (inventory == null)
                return Result<bool>.Failure("Not Found Invetory");

            _context.Inventories.Remove(inventory);
            await _context.SaveChangesAsync();
            return Result<bool>.Success(true);
        }
    }
}
