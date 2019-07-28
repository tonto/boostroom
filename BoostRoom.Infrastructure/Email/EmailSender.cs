using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using BoostRoom.Accounts.Domain;

namespace BoostRoom.Infrastructure.Email
{
    public class EmailSender : IEmailSender
    {
        private const string From = "b.stars.acc01@gmail.com";
        private const string Password = "gmail2019!";

        public Task SendEmailAsync(string toEmail, string subject, string htmlMessage)
        {
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                UseDefaultCredentials = false,
                EnableSsl = true,
                Credentials = new NetworkCredential(From, Password)
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(From)
            };

            mailMessage.IsBodyHtml = true;
            mailMessage.To.Add(toEmail);
            mailMessage.Subject = subject;
            mailMessage.Body = htmlMessage;

            return client.SendMailAsync(mailMessage);
        }
    }
}