using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using WebApplication4.Service_Layer.Interface;

namespace WebApplication4.Service_Layer.Implementation
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string to, string subject, string htmlMessage)
        {
            var fromEmail = _configuration["Smtp:Email"];
            var fromPassword = _configuration["Smtp:Password"];

            using var message = new MailMessage();
            message.From = new MailAddress(fromEmail);
            message.To.Add(new MailAddress(to));
            message.Subject = subject;
            message.Body = htmlMessage;
            message.IsBodyHtml = true;

            using var smtp = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential(fromEmail, fromPassword),
                EnableSsl = true
            };

            await smtp.SendMailAsync(message);
        }
    }
}
