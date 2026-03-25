using WebApplication4.Domain.Models;
namespace WebApplication4.Domain.IRepository
{
    public interface IMedicineRepo
    {

        Task<Medicine?> GetByIdAsync(int id);

        Task<IEnumerable<Medicine>> GetAllAsync();

        Task AddAsync(Medicine medicine);

        Task UpdateAsync(Medicine medicine);

        Task DeleteAsync(Medicine medicine);

        Task<bool> ExistsAsync(string name, string form, string strength);

        Task<ICollection<Medicine>> GetByNamesAsync(List<string> names);

        Task<int> GetMedicinesCountAsync();
        Task<int> GetMedicinesCountLow();

    }
}
