using System.Threading.Tasks;

namespace VividClub.Services
{
    public interface IEmailService : IService
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}