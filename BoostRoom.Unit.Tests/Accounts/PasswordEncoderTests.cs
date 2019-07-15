using BoostRoom.Infrastructure.Accounts;
using Xunit;

namespace BoostRoom.Unit.Tests.Accounts
{
    public class PasswordEncoderTests
    {
        [Fact]
        public void TestEncodeDecode()
        {
            const string password = "password123";
            
            var encoder = new AesPasswordEncoder();

            var encodedPassword = encoder.Encode(password);
            
            Assert.NotEqual(password, encodedPassword);

            var decodedPassword = encoder.Decode(encodedPassword);
            
            Assert.Equal(password, decodedPassword);
        }
    }
}