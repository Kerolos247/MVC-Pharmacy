using WebApplication4.Dto;
using WebApplication4.Models;

namespace WebApplication4.Service_Layer.Interface
{
    public interface ICategoryService
    {
        Task<Category?> GetByIdAsync(int id);

        // Get all categories
        Task<List<Category>> GetAllCategoriesAsync();

        // Create a new category and return the created entity
        Task<Category> CreateAsync(RequestCreateCategory dto);

        // Update category and return updated entity, or null if not found
        Task<Category?> UpdateAsync(int id, UpdateCategoryDto dto);

        // Delete category by Id, returns true if deleted, false if not found
        Task<bool> DeleteAsync(int id);
    }
}
