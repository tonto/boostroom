using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using BoostRoom.WebApp;
using BoostRoom.WebApp.Models;
using Microsoft.AspNetCore.Http;
using Xunit;

namespace BoostRoom.Integration.Tests.AccountsTests
{
    public class ClientsControllerTests : IClassFixture<WebApplicationFactoryForTest<Startup>>
    {
        private readonly HttpClient _client;

        public ClientsControllerTests(WebApplicationFactoryForTest<Startup> factory)
        {
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
            
            // TODO Assert event saved
        }
    }
}
