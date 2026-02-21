using WebApplication4.Application.Common.Results;
using WebApplication4.Application.Dto.Category;
using WebApplication4.Domain.Models;

namespace WebApplication4.Application.IServices
{
    public interface ICategoryService
    {
        Task<Result<List<Category>>> GetAllCategoriesAsync();
        Task<Result<Category?>> GetByIdAsync(int id);
        Task<Result<bool>> CreateAsync(RequestCreateCategory dto);
        Task<Result<bool>> UpdateAsync(int id, UpdateCategoryDto dto);
        Task<Result<bool>> DeleteAsync(int id);
    }
}
