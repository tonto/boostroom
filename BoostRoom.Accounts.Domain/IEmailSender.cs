using System.Threading.Tasks;

namespace BoostRoom.Accounts.Domain
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string toEmail, string subject, string htmlMessage);
    }
}