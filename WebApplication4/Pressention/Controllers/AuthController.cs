using System.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication4.Application.Dto;
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
        private readonly IEmailService _emailService;
        private readonly UserManager<Pharmacist> _userManager;

        public AuthController(
            IAuthService authService,
            ICategoryService categoryService, 
            ISupplierService supplierService,
            IPatientService patientService,
            IMedicineService medicineService,
            IPrescriptionService prescriptionService,
            IEmailService emailService,
            UserManager<Pharmacist> userManager)
        {
            _authService = authService;
            _supplierService = supplierService;
            _patientService = patientService;
            _medicineService = medicineService;
            _prescriptionService = prescriptionService;
            _emailService = emailService;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(PharmacistRegisterDto dto)
        {
            if (!ModelState.IsValid) return View(dto);

            var success = await _authService.RegisterAsync(dto);
            if (!success)
            {
                ModelState.AddModelError("", "Registration Failed!");
                return View(dto);
            }

            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(PharmacistLoginDto dto)
        {
            if (!ModelState.IsValid) return View(dto);

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

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _authService.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult ForgotPassword() => View();

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return RedirectToAction("ForgotPasswordConfirmation");

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var tokenBytes = System.Text.Encoding.UTF8.GetBytes(token);
            var encodedToken = Convert.ToBase64String(tokenBytes)
                                    .Replace("+", "-")
                                    .Replace("/", "_")
                                    .Replace("=", "");

            var resetLink = $"https://lynelle-coyish-unfrivolously.ngrok-free.dev/Auth/ResetPassword?token={encodedToken}&email={model.Email}";

            await _emailService.SendEmailAsync(
                model.Email,
                "Reset Password",
                $"Click <a href='{resetLink}'>here</a> to reset your password."
            );

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
        public async Task<IActionResult> ResetPassword(ResetPasswordDto model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                TempData["Error"] = "User not found.";
                return RedirectToAction("ForgotPassword");
            }

            var base64Token = model.Token.Replace("-", "+").Replace("_", "/");
            switch (base64Token.Length % 4)
            {
                case 2: base64Token += "=="; break;
                case 3: base64Token += "="; break;
            }
            var decodedToken = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(base64Token));

            var result = await _userManager.ResetPasswordAsync(user, decodedToken, model.Password);

            if (result.Succeeded)
            {
                TempData["Success"] = "Password reset successfully!";
                return RedirectToAction("Login");
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);

            return View(model);
        }
    }
}
