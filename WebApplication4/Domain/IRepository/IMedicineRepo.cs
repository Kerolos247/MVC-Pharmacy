using Microsoft.AspNetCore.Mvc;
using WebApplication4.Application.Dto;
using WebApplication4.Domain.Models;

namespace WebApplication4.Domain.IRepository
{
    public interface IMedicineRepo
    {

        Task<Medicine?> GetByIdAsync(int id);
        Task<List<Medicine>> GetAllMedicinesAsync();
        Task<Result<bool>> CreateAsync(RequestCreateMedcine medicine);
        Task<Result<bool>> UpdateAsync(int id, UpdateMedcineDto medicine);
        Task<Result<bool>> DeleteAsync(int id);
    }
}
