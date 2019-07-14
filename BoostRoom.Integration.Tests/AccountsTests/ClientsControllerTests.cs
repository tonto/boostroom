using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using BoostRoom.Accounts.Domain.ClientAggregate;
using BoostRoom.WebApp;
using BoostRoom.WebApp.Models;
using Microsoft.AspNetCore.Http;
using Tactical.DDD.Testing;
using Xunit;

namespace BoostRoom.Integration.Tests.AccountsTests
{
    public class ClientsControllerTests : IClassFixture<ClientsControllerApplicationFactory<Startup>>
    {
        private readonly ClientsControllerApplicationFactory<Startup> _factory;
        private readonly HttpClient _client;

        public ClientsControllerTests(ClientsControllerApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
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
        public async Task Register_Pass_Validation()
        {
            var response = await _client.PostAsJsonAsync("/api/accounts/clients/register", new RegisterClientDto
            {
                Username = "michael",
                Email = "me@mail.com",
                Password = "pass123",
                FirstName = "John",
                LastName = "Doe",
                AddressLine = "Some Address",
                City = "London",
                Zip = "1234",
                Country = "UK",
                SubscribeToOffers = true 
            });

            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

            var events = await _factory.EventStore.LoadAllEventsAsync();

            events.ExpectOne<ClientRegistered>(e =>
            {
                Assert.NotNull(e.AggregateId);
            });
        }

        // TODO - Create test for duplicate username/email (this will test the unique projection)

        // TODO - Check if event.Created at is loaded from db
    }
}
