using WebApplication4.Application.Dto;
using WebApplication4.Domain.Models;

namespace WebApplication4.Domain.IRepository
{
    public interface ICategoryRepo
    {
        Task<Category?> GetByIdAsync(int id);

        Task<List<Category>> GetAllCategoriesAsync();

        Task<Category> CreateAsync(RequestCreateCategory dto);

       
        Task<Category?> UpdateAsync(int id, UpdateCategoryDto dto);

       
        Task<bool> DeleteAsync(int id);
    }
}
