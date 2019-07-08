using BoostRoom.Accounts.Domain.ClientAggregate;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Utilities;
using Tactical.DDD.Testing;
using Xunit;

namespace BoostRoom.Accounts.Tests.UnitTests
{
    public class WhenRegisteringClient : AggregateSpecification<Client, ClientId>
    {
        private readonly ClientId _clientId = new ClientId();
        private readonly string _username = "john";
        private readonly string _email = "john@mail.com";
        private readonly string _encryptedPassword = "xxxyyyzzz";
        private readonly string _firstName = "John";
        private readonly string _lastName = "Doe";
        private readonly string _addressLine = "Sunrise str";
        private readonly string _city = "London";
        private readonly string _zip = "B444";
        private readonly string _country = "USA";
        private readonly bool _subscribedToOffers = true;

        protected override Client Given()
        {
            return null;
        }

        protected override void When()
        {
            Aggregate = Client.FromDetails(
                _clientId,
                _username, 
                _email, 
                _encryptedPassword,
                _firstName,
                _lastName,
                _addressLine, 
                _city, 
                _zip, 
                _country,
                _subscribedToOffers);
        }

        [Fact]
        public void ClientIsRegistered()
        {
            ProducedEvents.ExpectOne<ClientRegistered>(e =>
            {
                Assert.NotNull(e.AggregateId);
                Assert.Equal(_username, e.Username);
                Assert.Equal(_email, e.Email);
                Assert.Equal(_encryptedPassword, e.EncryptedPassword);
                Assert.Equal(_firstName, e.FirstName);
                Assert.Equal(_lastName, e.LastName);
                Assert.Equal(_addressLine, e.AddressLine);
                Assert.Equal(_city, e.City);
                Assert.Equal(_zip, e.Zip);
                Assert.Equal(_country, e.Country);
                Assert.Equal(_subscribedToOffers, e.SubscribedToOffers);
            });
        }
    }
}