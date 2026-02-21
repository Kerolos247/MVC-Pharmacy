using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebApplication4.Application.Dto.Auth;
using WebApplication4.Application.IServices;
using WebApplication4.Domain.Models;

namespace WebApplication4.Infrastructure.Services
{
    public class AuthService : IAuthService
    {

        private readonly UserManager<Pharmacist> _userManager;
        private readonly SignInManager<Pharmacist> _signInManager;
        private readonly IEmailService _emailService;

        public AuthService(UserManager<Pharmacist> userManager,
                           SignInManager<Pharmacist> signInManager, IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
        }

        public async Task<AuthResult> RegisterAsync(PharmacistRegisterDto dto)
        {
            AuthResult authResult = new AuthResult();
            var user = new Pharmacist
            {
                UserName = dto.Email,
                Email = dto.Email,
                FullName = dto.FullName
            };

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    authResult.Message += error.Description;
                    authResult.Message += "/n";
                }
                authResult.Success = false;
                return authResult;
            }


            await _userManager.AddToRoleAsync(user, "Pharmacist");
            return new AuthResult { Success = false, Message = "Login Is Successful" };
        }


        public async Task<AuthResult> LoginAsync(PharmacistLoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user == null)
                return new AuthResult
                {
                    Success = false,
                    Message = "Invalid Email Or Password"
                };

            var result = await _signInManager.PasswordSignInAsync(user, dto.Password, false, true);

            if (result.IsLockedOut)
            {
                string subject = "⚠️ Multiple failed login attempts detected";

                string body = "If this was you, please use \"Forgot Password\" to reset your password.\r\n" +
                              "If this was not you, please change your password immediately to secure your account.";

                await _emailService.SendEmailAsync(dto.Email, subject, body);

                return new AuthResult
                {
                    Success = false,
                    Message = "Your account has been temporarily locked due to multiple failed login attempts."
                };
            }

            if (!result.Succeeded)
                return new AuthResult
                {
                    Success = false,
                    Message = "Invalid Email Or Password"
                };


            var roles = await _userManager.GetRolesAsync(user);

            var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();

            if (string.IsNullOrEmpty(role))
            {
                return new AuthResult
                {
                    Success = false,
                    Message = "No role assigned to this user."
                };
            }

            return new AuthResult
            {
                Success = true,
                Message = "Login successful",
                Role = role
            };

        }


        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }
        public async Task<Pharmacist?> FindUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<IdentityResult> ResetPasswordAsync(string email, string token, string newPassword)
        {
            var user = await FindUserByEmailAsync(email);
            if (user == null)
                return IdentityResult.Failed(new IdentityError { Description = "User not found." });


            var base64Token = token.Replace("-", "+").Replace("_", "/");
            switch (base64Token.Length % 4)
            {
                case 2: base64Token += "=="; break;
                case 3: base64Token += "="; break;
            }
            var decodedToken = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(base64Token));

            return await _userManager.ResetPasswordAsync(user, decodedToken, newPassword);
        }
        public async Task SendPasswordResetEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return;

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);


            var tokenBytes = System.Text.Encoding.UTF8.GetBytes(token);
            var encodedToken = Convert.ToBase64String(tokenBytes)
                                    .Replace("+", "-")
                                    .Replace("/", "_")
                                    .Replace("=", "");

            var resetLink = $"https://lynelle-coyish-unfrivolously.ngrok-free.dev/Auth/ResetPassword?token={encodedToken}&email={email}";

            await _emailService.SendEmailAsync(
                email,
                "Reset Password",
                $"Click <a href='{resetLink}'>here</a> to reset your password."
            );
        }

    }
}
