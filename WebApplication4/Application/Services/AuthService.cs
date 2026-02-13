using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebApplication4.Application.Dto;
using WebApplication4.Application.IServices;
using WebApplication4.Domain.Models;

namespace WebApplication4.Application.Services
{
    public class AuthService : IAuthService
    {

        private readonly UserManager<Pharmacist> _userManager;
        private readonly SignInManager<Pharmacist> _signInManager;

        public AuthService(UserManager<Pharmacist> userManager,
                           SignInManager<Pharmacist> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<bool> RegisterAsync(PharmacistRegisterDto dto)
        {

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
                    Console.WriteLine(error.Description);
                }
                return false;
            }


            await _userManager.AddToRoleAsync(user, "Pharmacist");
            return true;
        }


        public async Task<bool> LoginAsync(PharmacistLoginDto dto)
        {
            var result = await _signInManager.PasswordSignInAsync(dto.Email, dto.Password, true, false);
            return result.Succeeded;
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

    }
}
