using System;
using System.Linq;
using BoostRoom.Accounts.Domain.ClientAggregate;
using Xunit;

namespace BoostRoom.Accounts.Tests.UnitTests
{
    public class ClientTests
    {
        [Fact]
        public void TestClient_RegisterWithDetails_Results_In_ClientRegistered_Event()
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
            var id = new ClientId();

            var client = Client.RegisterWithDetails(
                id,
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

            // throws if more than one
            // throws if no elems
            // returns event if only one
            var @event = client.DomainEvents.Single(e =>
                e is ClientRegistered
            );
            
            var contains = client.DomainEvents.Contains(new ClientRegistered(id, username, email, encryptedPassword,
                firstName,
                lastName, addressLine, city, zip, country, subscribeToOffers));

            Assert.True(contains);

            // TODO - Test event, not side effects

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