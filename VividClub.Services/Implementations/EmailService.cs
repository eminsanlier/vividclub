using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using VividClub.Services.Models;

namespace VividClub.Services
{

    public class EmailService : IEmailService
    {
        private EmailSettingsModel settings { get; set; }

        public EmailService(EmailSettingsModel config)
        {
            settings = config;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            using (var client = new SmtpClient(settings.Smtp, settings.Port))
            {
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(settings.ApplicationEmail, settings.ApplicationEmailPassword);

                var mailMessage = new MailMessage();
                mailMessage.To.Add(email);
                mailMessage.From = new MailAddress(settings.ApplicationEmail, settings.ApplicationName);
                mailMessage.Subject = subject;
                mailMessage.Body = message;
                mailMessage.IsBodyHtml = false;

                await client.SendMailAsync(mailMessage);
            }
        }
    }
}