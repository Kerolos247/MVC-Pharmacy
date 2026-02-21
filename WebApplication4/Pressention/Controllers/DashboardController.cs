using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication4.Application.Dto.Dashboard;
using WebApplication4.Application.IServices;
using WebApplication4.Application.Services;

namespace WebApplication4.Pressention.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly IMedicineService _medicineService;
        private readonly IPatientService _patientService;
        private readonly ISupplierService _supplierService;
        private readonly IPrescriptionService _prescriptionService;
        public DashboardController(
           ISupplierService supplierService,
           IPatientService patientService,
           IMedicineService medicineService,
           IPrescriptionService prescriptionService)
        {
            _supplierService = supplierService;
            _patientService = patientService;
            _medicineService = medicineService;
            _prescriptionService = prescriptionService;
        }

        [HttpGet]
        public IActionResult AdminDashboard() => View();

        [HttpGet]
        public async Task<IActionResult> Pharmacist()
        {
            var model = new DashboardViewModel
            {
                MedicinesCount = (await _medicineService.GetAllMedicinesAsync()).Data?.Count ?? 0,
                PatientsCount = (await _patientService.GetAllPatientsAsync()).Data?.Count ?? 0,
                SuppliersCount = (await _supplierService.GetAllSuppliersAsync()).Data?.Count ?? 0,
                PrescriptionsCount = (await _prescriptionService.GetAllPrescriptionsAsync()).Data?.Count ?? 0
            };


            return View(model);
        }
    }
}
