using Microsoft.AspNetCore.Mvc;
using WebApplication4.Application.Common.Results;
using WebApplication4.Application.Dto.Medcine;
using WebApplication4.Domain.Models;

namespace WebApplication4.Domain.IRepository
{
    public interface IMedicineRepo
    {

        Task<Medicine?> GetByIdAsync(int id);

        Task<List<Medicine>> GetAllAsync();

        Task AddAsync(Medicine medicine);

        Task UpdateAsync(Medicine medicine);

        Task DeleteAsync(Medicine medicine);

        Task<bool> ExistsAsync(string name, string form, string strength);

        Task<List<Medicine>> GetByNamesAsync(List<string> names);
    }
}
