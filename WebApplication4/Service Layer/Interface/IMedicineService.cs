using Microsoft.AspNetCore.Mvc;
using WebApplication4.Dto;
using WebApplication4.Models;

namespace WebApplication4.Service_Layer.Interface
{
    public interface IMedicineService
    {
       
        Task<Medicine?> GetByIdAsync(int id);
        Task<List<Medicine>> GetAllMedicinesAsync();
        Task<Medicine> CreateAsync(RequestCreateMedcine medicine);
        Task<Medicine?> UpdateAsync(int id, UpdateMedcineDto medicine);
        Task<bool> DeleteAsync(int id);
    }
}
