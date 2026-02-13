using WebApplication4.Application.Dto;
using WebApplication4.Domain.Models;

namespace WebApplication4.Application.IServices
{
    public interface ICategoryService
    {
        Task<Category?> GetByIdAsync(int id);
        Task<List<Category>> GetAllCategoriesAsync();
        Task<Category> CreateAsync(RequestCreateCategory dto);
        Task<Category?> UpdateAsync(int id, UpdateCategoryDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
