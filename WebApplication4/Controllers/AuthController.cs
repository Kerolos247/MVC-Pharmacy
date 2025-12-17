using Microsoft.AspNetCore.Mvc;
using WebApplication4.Dto;
using WebApplication4.Service_Layer.Interface;

namespace WebApplication4.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly ICategoryService _categoryService;
        private readonly ISupplierService _supplierService;
        private readonly IPatientService _patientService;
        private readonly IMedicineService _medicineService;
        private readonly IPrescriptionService _prescriptionService;

        public AuthController(
            IAuthService authService,
            ICategoryService categoryService,
            ISupplierService supplierService,
            IPatientService patientService,
            IMedicineService medicineService, IPrescriptionService prescriptionService)
        {
            _authService = authService;
            _categoryService = categoryService;
            _supplierService = supplierService;
            _patientService = patientService;
            _medicineService = medicineService;
            _prescriptionService= prescriptionService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(PharmacistRegisterDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var success = await _authService.RegisterAsync(dto);

            if (!success)
            {
                ModelState.AddModelError("", "Registration Failed!");
                return View(dto);
            }

            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(PharmacistLoginDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var success = await _authService.LoginAsync(dto);

            if (!success)
            {
                ModelState.AddModelError("", "Invalid email or password");
                return View(dto);
            }

            return RedirectToAction("Main");
        }

        [HttpGet]
        public async Task<IActionResult> Main()
        {
            var model = new DashboardViewModel
            {
                MedicinesCount = (await _medicineService.GetAllMedicinesAsync()).Count,
                PatientsCount = (await _patientService.GetAllPatientsAsync()).Count,
                SuppliersCount = (await _supplierService.GetAllSuppliersAsync()).Count,
                PrescriptionsCount = (await _prescriptionService.GetAllPrescriptionsAsync()).Count
            };

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _authService.LogoutAsync();
            return RedirectToAction("Login");
        }
    }
}
