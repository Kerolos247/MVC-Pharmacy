using Microsoft.EntityFrameworkCore;
using WebApplication4.DB;
using WebApplication4.Dto;
using WebApplication4.Models;
using WebApplication4.Service_Layer.Interface;

namespace WebApplication4.Service_Layer.Implementation
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;

        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _context.Categories
                .Include(c => c.Medicines)
                .FirstOrDefaultAsync(c => c.CategoryId == id);
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await _context.Categories
                .Include(c => c.Medicines)
                .ToListAsync();
        }

        public async Task<Category> CreateAsync(RequestCreateCategory dto)
        {
            var category = new Category
            {
                Name = dto.Name,
                Medicines = new List<Medicine>()
            };

            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<Category?> UpdateAsync(int id, UpdateCategoryDto dto)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return null;

            if (!string.IsNullOrEmpty(dto.Name))
                category.Name = dto.Name;

            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return false;

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
