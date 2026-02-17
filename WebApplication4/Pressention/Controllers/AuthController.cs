using System.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication4.Application.Dto.Dashboard;
using WebApplication4.Application.Dto.Auth;
using WebApplication4.Application.IServices;
using WebApplication4.Domain.Models;

namespace WebApplication4.Pressention.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly ISupplierService _supplierService;
        private readonly IPatientService _patientService;
        private readonly IMedicineService _medicineService;
        private readonly IPrescriptionService _prescriptionService;

        public AuthController(
            IAuthService authService, 
            ISupplierService supplierService,
            IPatientService patientService,
            IMedicineService medicineService,
            IPrescriptionService prescriptionService)
        {
            _authService = authService;
            _supplierService = supplierService;
            _patientService = patientService;
            _medicineService = medicineService;
            _prescriptionService = prescriptionService;
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(PharmacistRegisterDto dto)
        {
            if (!ModelState.IsValid) return View(dto);

            var success = await _authService.RegisterAsync(dto);
            if (!success.Success)
            {
                ModelState.AddModelError("",success.Message);
                return View(dto);
            }

            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(PharmacistLoginDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var success = await _authService.LoginAsync(dto);

            if(!success.Success)
            {
                ModelState.AddModelError("",success.Message);
                return View(dto);
            }
            if(success.Message == "Account Is Block")
            {
                ModelState.AddModelError("", success.Message);
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

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _authService.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult ForgotPassword() => View();

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto model, [FromServices] IAuthService authService)
        {
            if (!ModelState.IsValid)
                return View(model);

            await authService.SendPasswordResetEmailAsync(model.Email);

            return RedirectToAction("ForgotPasswordConfirmation");
        }

        [HttpGet]
        public IActionResult ForgotPasswordConfirmation() => View();

        [HttpGet]
        public IActionResult ResetPassword(string token, string email)
        {
            if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(email))
            {
                TempData["Error"] = "Invalid reset link.";
                return RedirectToAction("ForgotPassword");
            }

            var model = new ResetPasswordDto
            {
                Token = token,
                Email = email
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto model, [FromServices] IAuthService authService)
        {
            if (!ModelState.IsValid) return View(model);

            var result = await authService.ResetPasswordAsync(model.Email, model.Token, model.Password);

            if (result.Succeeded)
            {
                TempData["Success"] = "Password reset successfully!";
                return RedirectToAction("Login");
            }

            
            if (result.Errors.Any(e => e.Description == "User not found."))
            {
                TempData["Error"] = "User not found.";
                return RedirectToAction("ForgotPassword");
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);

            return View(model);
        }

    }
}
