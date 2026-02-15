using WebApplication4.Application.Dto.Auth;

namespace WebApplication4.Application.IServices
{
    public interface IAuthService
    {
        Task<AuthResult> RegisterAsync(PharmacistRegisterDto dto);
        Task<AuthResult> LoginAsync(PharmacistLoginDto dto);
        Task LogoutAsync();
    }
}
