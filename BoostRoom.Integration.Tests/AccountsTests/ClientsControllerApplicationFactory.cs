using System;
using System.Reflection;
using BoostRoom.Accounts.Application;
using BoostRoom.Accounts.Domain;
using BoostRoom.Accounts.Domain.ClientAggregate;
using BoostRoom.Infrastructure.Accounts;
using BoostRoom.Infrastructure.Accounts.RavenDB;
using EventStore.ClientAPI;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Tactical.DDD.EventSourcing;

namespace BoostRoom.Integration.Tests.AccountsTests
{
    public class ClientsControllerApplicationFactory<TStartup> : WebApplicationFactory<TStartup>
        where TStartup : class
    {

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.AddMediatR(typeof(AccountsApplicationAssembly));

                services.AddSingleton<IPasswordEncoder, AesPasswordEncoder>();
                services.AddSingleton<IUniqueAccountsRepository, UniqueAccountsRepository>();
                services.AddSingleton<IClientsRepository, ClientsRepository>();

                services.AddScoped<RegistrationService, RegistrationService>();
            });
        }
    }
}