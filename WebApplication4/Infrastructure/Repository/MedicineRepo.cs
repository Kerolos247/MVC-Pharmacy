using Microsoft.EntityFrameworkCore;
using WebApplication4.Domain.IRepository;
using WebApplication4.Domain.Models;
using WebApplication4.Infrastructure.DB;
public class MedicineRepo : IMedicineRepo
{
    private readonly ApplicationDbContext _context;

    public MedicineRepo(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Medicine?> GetByIdAsync(int id)
    {
        return await _context.Medicines
            .Include(m => m.Category)
            .Include(m => m.Supplier)
            .Include(m => m.Inventory)
            .FirstOrDefaultAsync(m => m.MedicineId == id);
    }

    public async Task<List<Medicine>> GetAllAsync()
    {
        return await _context.Medicines
            .Include(c => c.Category)
            .Include(m => m.Supplier)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task AddAsync(Medicine medicine)
    {
        await _context.Medicines.AddAsync(medicine);
    }

    public Task UpdateAsync(Medicine medicine)
    {
        _context.Medicines.Update(medicine);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Medicine medicine)
    {
        _context.Medicines.Remove(medicine);
        return Task.CompletedTask;
    }

    public Task<bool> ExistsAsync(string name, string form, string strength)
    {
        return _context.Medicines.AnyAsync(m =>
            m.Name == name &&
            m.DosageForm == form &&
            m.Strength == strength);
    }

    public async Task<List<Medicine>> GetByNamesAsync(List<string> names)
    {
        var medicinesFromDb = await _context.Medicines
            .Where(m => names.Contains(m.Name))
            .AsNoTracking()
            .ToListAsync();

       
        var medicinesOrdered = new List<Medicine>();

        foreach (var name in names)
        {
            var med = medicinesFromDb.FirstOrDefault(m => m.Name == name);
            if (med != null)
                medicinesOrdered.Add(med);
        }

        return medicinesOrdered;

    }
}
