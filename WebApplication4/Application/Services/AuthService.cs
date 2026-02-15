using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebApplication4.Application.Dto.Auth;
using WebApplication4.Application.IServices;
using WebApplication4.Domain.Models;

namespace WebApplication4.Application.Services
{
    public class AuthService : IAuthService
    {

        private readonly UserManager<Pharmacist> _userManager;
        private readonly SignInManager<Pharmacist> _signInManager;
        private readonly IEmailService _emailService;

        public AuthService(UserManager<Pharmacist> userManager,
                           SignInManager<Pharmacist> signInManager,IEmailService emailService)
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
                     authResult.Message+=error.Description;
                     authResult.Message+= "/n";
                }
                authResult.Success = false;
                return authResult;
            }


            await _userManager.AddToRoleAsync(user, "Pharmacist");
            return new AuthResult { Success = false, Message = "Login Is Successful" };
        }


        public async Task<AuthResult> LoginAsync(PharmacistLoginDto dto)
        {
            var result = await _signInManager.PasswordSignInAsync(dto.Email, dto.Password, false, true);

            if (result.IsLockedOut)
            {
               
                string subject = "⚠️ Multiple failed login attempts detected";

               
                string body = "If this was you, please use \"Forgot Password\" to reset your password.\r\n" +
                              "If this was not you, please change your password immediately to secure your account.";

                
                await _emailService.SendEmailAsync(dto.Email, subject, body);

                return new AuthResult
                {
                    Success = false,
                    Message = "Your account has been temporarily locked due to multiple failed login attempts.\r\n" +
                              "Please try again later or contact the administrator for assistance."
                };
            }

            if (!result.Succeeded)
                return new AuthResult { Success = false, Message = "Invalid Email Or Password" };

            return new AuthResult { Success = true, Message = "Login successful" };
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

    }
}
