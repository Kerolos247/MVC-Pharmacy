using Microsoft.AspNetCore.Identity;
using WebApplication4.Application.Dto.Auth;
using WebApplication4.Domain.Models;
using WebApplication4.Application.Common.Results;

namespace WebApplication4.Application.IServices
{
    public interface IAuthService
    {
        
        Task<AuthResult> RegisterAsync(PharmacistRegisterDto dto);
        Task<AuthResult> LoginAsync(PharmacistLoginDto dto);
        Task LogoutAsync();

        Task<IdentityResult> ResetPasswordAsync(string email, string token, string newPassword);
        Task<Pharmacist?> FindUserByEmailAsync(string email);

        Task SendPasswordResetEmailAsync(string email);
    }
}
