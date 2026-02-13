using WebApplication4.Application.Dto;
using WebApplication4.Application.IServices;
using WebApplication4.Domain.IRepository;
using WebApplication4.Domain.Models;

namespace WebApplication4.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepo _categoryRepo;

        public CategoryService(ICategoryRepo categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

       
        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _categoryRepo.GetByIdAsync(id);
        }

       
        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await _categoryRepo.GetAllCategoriesAsync();
        }

       
        public async Task<Category> CreateAsync(RequestCreateCategory dto)
        {
            return await _categoryRepo.CreateAsync(dto);
        }

        public async Task<Category?> UpdateAsync(int id, UpdateCategoryDto dto)
        {
            return await _categoryRepo.UpdateAsync(id, dto);
        }

       
        public async Task<bool> DeleteAsync(int id)
        {
            return await _categoryRepo.DeleteAsync(id);
        }
    }
}
