using WebApplication4.Application.Dto;

namespace WebApplication4.Application.IServices
{
    public interface IAuthService
    {
        Task<bool> RegisterAsync(PharmacistRegisterDto dto);
        Task<bool> LoginAsync(PharmacistLoginDto dto);
        Task LogoutAsync();
    }
}
