using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication4.Dto;
using WebApplication4.Models;
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
        private readonly IEmailService _emailService;
        private readonly UserManager<Pharmacist> _userManager;
        private readonly IConfiguration _configuration;

        public AuthController(
            IAuthService authService,
            ICategoryService categoryService,
            ISupplierService supplierService,
            IPatientService patientService,
            IMedicineService medicineService,
            IPrescriptionService prescriptionService,
            IEmailService emailService,
            UserManager<Pharmacist> userManager, IConfiguration configuration)
        {
            _authService = authService;
            _categoryService = categoryService;
            _supplierService = supplierService;
            _patientService = patientService;
            _medicineService = medicineService;
            _prescriptionService = prescriptionService;
            _emailService = emailService;
            _userManager = userManager;
            _configuration = configuration;
        }

        // ---------------- Register ----------------
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

        // ---------------- Login ----------------
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

        // ---------------- Dashboard ----------------
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

        // ---------------- Logout ----------------
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _authService.LogoutAsync();
            return RedirectToAction("Login");
        }

        // ---------------- Forgot Password ----------------
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return RedirectToAction("ForgotPasswordConfirmation");

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            // استخدام IP او domain صالح
            var host = _configuration["LocalHost"];
            var resetLink = $"http://{host}/Auth/ResetPassword?token={Uri.EscapeDataString(token)}&email={model.Email}";

            await _emailService.SendEmailAsync(
                model.Email,
                "Reset Password",
                $"Click <a href='{resetLink}'>here</a> to reset your password."
            );

            return RedirectToAction("ForgotPasswordConfirmation");
        }

        [HttpGet]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ResetPassword(string token, string email)
        {
            var model = new ResetPasswordDto { Token = token, Email = email };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null) return RedirectToAction("Login");

            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);

            if (result.Succeeded)
                return RedirectToAction("Login");

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            return View(model);
        }

    }
}
