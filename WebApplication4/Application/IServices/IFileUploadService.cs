namespace WebApplication4.Application.IServices
{
    public interface IFileUploadService
    {
        Task<string> UploadAsync(IFormFile file);
    }
}
