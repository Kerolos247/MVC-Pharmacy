using WebApplication4.Application.Dto.Category;
using WebApplication4.Domain.Models;

namespace WebApplication4.Domain.IRepository
{
    public interface ICategoryRepo
    {
        Task<Category?> GetByIdAsync(int id);
        Task<List<Category>> GetAllAsync();
        Task AddAsync(Category category);
        Task UpdateAsync(Category category);
        Task DeleteAsync(Category category);
    }
}
