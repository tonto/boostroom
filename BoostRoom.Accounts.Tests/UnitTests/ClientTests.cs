using BoostRoom.Accounts.Domain.ClientAggregate;
using Xunit;

namespace BoostRoom.Accounts.Tests.UnitTests
{
    public class ClientTests
    {
        [Fact]
        public void TestClient_RegisterWithDetails()
        {
            const string username = "anes";
            const string email = "anes@mail.com";
            const string encryptedPassword = "xy2039009ds";
            const string firstName = "Anes";
            const string lastName = "Hasicic";
            const string addressLine = "Musterstrasse 23";
            const string city = "Sarajevo";
            const string zip = "71000";
            const string country = "BiH";
            const bool subscribeToOffers = true;
            
            var client = Client.RegisterWithDetails(
                username,
                email,
                encryptedPassword,
                firstName,
                lastName,
                addressLine,
                city,
                zip,
                country,
                subscribeToOffers
           );

            Assert.Equal(city, client.Address.City);
            Assert.Equal(country, client.Address.Country);
            Assert.Equal(zip, client.Address.Zip);
            Assert.Equal(addressLine, client.Address.AddressLine);
            
            Assert.Equal(firstName, client.FullName.FirstName);
            Assert.Equal(lastName, client.FullName.LastName);
            
            Assert.Equal(username, client.Username.Value);
            Assert.Equal(email, client.Email.Value);
            
            Assert.Equal(encryptedPassword, client.Password.Encrypted);
        }
    }
}