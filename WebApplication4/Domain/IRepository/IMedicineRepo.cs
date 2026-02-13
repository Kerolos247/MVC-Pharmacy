using Microsoft.AspNetCore.Mvc;
using WebApplication4.Application.Dto;
using WebApplication4.Domain.Models;

namespace WebApplication4.Domain.IRepository
{
    public interface IMedicineRepo
    {

        Task<Medicine?> GetByIdAsync(int id);
        Task<List<Medicine>> GetAllMedicinesAsync();
        Task<Medicine> CreateAsync(RequestCreateMedcine medicine);
        Task<Medicine?> UpdateAsync(int id, UpdateMedcineDto medicine);
        Task<bool> DeleteAsync(int id);
    }
}
