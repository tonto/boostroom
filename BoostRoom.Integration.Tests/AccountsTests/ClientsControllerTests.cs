using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using BoostRoom.Accounts.Domain.ClientAggregate;
using BoostRoom.WebApp;
using BoostRoom.WebApp.Models;
using EventStore.ClientAPI;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Raven.Client.Documents;
using Tactical.DDD.EventSourcing;
using Tactical.DDD.Testing;
using Xunit;

namespace BoostRoom.Integration.Tests.AccountsTests
{
    public class ClientsControllerTests : 
        EmbeddedRavenDbTest,
        IClassFixture<EmbeddedEventStoreFixture>,
        IClassFixture<ClientsControllerApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        private BoostRoom.Infrastructure.EventStore _eventStore;

        public ClientsControllerTests(ClientsControllerApplicationFactory<Startup> factory, EmbeddedEventStoreFixture esFixture)
        {
            _client = factory.WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        
                        _eventStore = new BoostRoom.Infrastructure.EventStore(esFixture.Connection);
                        
                        // TODO - What are the lifetimes of these ??????
                        services.AddSingleton<IEventStore>(_eventStore);
                        services.AddSingleton<BoostRoom.Infrastructure.IEventStore>(_eventStore);
                        services.AddSingleton<IDocumentStore>(DocumentStore);
                    });
                })
                .CreateClient();
        }

        [Fact]
        public async Task Register_Validates_Username()
        {
            var response = await _client.PostAsJsonAsync("/api/accounts/clients/register", new RegisterClientDto());
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Register_Validates_Username_Length()
        {
            var response = await _client.PostAsJsonAsync("/api/accounts/clients/register", new RegisterClientDto
            {
                Username = "foo"
            });

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Register_Validates_FirstName()
        {
            var response = await _client.PostAsJsonAsync("/api/accounts/clients/register", new RegisterClientDto
            {
                Username = "foo",
                FirstName = ""
            });

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Register_Registers_Client()
        {
            const string username = "michael";
            const string email = "me@mail.com";
            const string password = "pass123";
            const string firstName = "John";
            const string lastName = "Doe";
            const string addressLine = "Some Address";
            const string city = "London";
            const string zip = "1234";
            const string country = "UK";
            const bool subscribeToOffers = true;

            var response = await _client.PostAsJsonAsync("/api/accounts/clients/register", new RegisterClientDto
            {
                Username = username,
                Email = email,
                Password = password,
                FirstName = firstName,
                LastName = lastName,
                AddressLine = addressLine,
                City = city,
                Zip = zip,
                Country = country,
                SubscribeToOffers = subscribeToOffers
            });
            
            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

            var events = await _eventStore.LoadAllEventsAsync();

            events.ExpectOne<ClientRegistered>(e =>
            {
                Assert.False(String.IsNullOrEmpty(e.AggregateId));
                Assert.True(e.CreatedAt > DateTime.MinValue);
                Assert.Equal(username, e.Username);
                Assert.Equal(email, e.Email);
                Assert.False(String.IsNullOrEmpty(e.EncryptedPassword));
                Assert.Equal(firstName, e.FirstName);
                Assert.Equal(lastName, e.LastName);
                Assert.Equal(addressLine, e.AddressLine);
                Assert.Equal(city, e.City);
                Assert.Equal(zip, e.Zip);
                Assert.Equal(country, e.Country);
                Assert.Equal(subscribeToOffers, e.SubscribedToOffers);
            });
        }

        // TODO - Create test for duplicate username/email (this will test unique projection)
    }
}