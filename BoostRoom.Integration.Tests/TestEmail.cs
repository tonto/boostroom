using System.Threading.Tasks;
using BoostRoom.Infrastructure.Email;
using Xunit;

namespace BoostRoom.Integration.Tests
{
    public class TestEmail
    {
        [Fact]
        public async Task EmailSenderSendsEmail()
        {
            var sender = new EmailSender();

            var code = "some-code";
            var mail = $@"
Your BoostRoom account has been created.<br /><br />
Click <a href='http://18.188.124.36/accounts/confirm-email/{code}'>here</a> to confirm your email.<br /><br />
BoostRoom";

            await sender.SendEmailAsync("me@anes.io", "BoostRoom Email Confirmation", mail);
        }
    }
}