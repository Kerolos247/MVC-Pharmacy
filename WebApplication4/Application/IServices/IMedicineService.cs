using WebApplication4.Application.Dto;
using WebApplication4.Domain.Models;

namespace WebApplication4.Application.IServices
{
    public interface IMedicineService
    {
        Task<List<Medicine>> GetAllMedicinesAsync();
        Task<Medicine?> GetByIdAsync(int id);
        Task<Medicine> CreateAsync(RequestCreateMedcine medicine);
        Task<Medicine?> UpdateAsync(int id, UpdateMedcineDto medicine);
        Task<bool> DeleteAsync(int id);
    }
}
