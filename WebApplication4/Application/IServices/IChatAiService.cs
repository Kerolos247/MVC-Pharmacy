namespace WebApplication4.Application.IServices
{
    public interface IChatAiService
    {
        Task<string> Ask(string question);
    }
}
