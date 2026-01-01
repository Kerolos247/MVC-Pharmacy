namespace WebApplication4.Service_Layer.Interface
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject, string htmlMessage);
    }
}
