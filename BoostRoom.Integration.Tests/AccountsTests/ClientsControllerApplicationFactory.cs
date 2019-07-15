using System;
using System.Reflection;
using BoostRoom.Accounts.Application;
using BoostRoom.Accounts.Domain;
using BoostRoom.Accounts.Domain.ClientAggregate;
using BoostRoom.Infrastructure.Accounts;
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
        public BoostRoom.Infrastructure.EventStore EventStore { get; private set; }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.AddMediatR(typeof(AccountsApplicationAssembly));

                services.AddSingleton<IPasswordEncoder, AesPasswordEncoder>();
                services.AddSingleton<IUniqueAccountsProjection, UniqueAccountsProjection>();
                services.AddSingleton<IClientsRepository, ClientsRepository>();

                var conn = EventStoreConnection.Create(new Uri("tcp://admin:changeit@localhost:1113"),
                              "testEventStoreConnection");

                conn.ConnectAsync().Wait();

                EventStore = new BoostRoom.Infrastructure.EventStore(conn);

                services.AddSingleton<IEventStore>(EventStore);

                services.AddScoped<RegistrationService, RegistrationService>();
            });
        }
    }
}