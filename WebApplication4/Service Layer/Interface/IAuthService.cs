using WebApplication4.Dto;

namespace WebApplication4.Service_Layer.Interface
{
    public interface IAuthService
    {
        Task<bool> RegisterAsync(PharmacistRegisterDto dto);
        Task<bool> LoginAsync(PharmacistLoginDto dto);
        Task LogoutAsync();
    }
}
