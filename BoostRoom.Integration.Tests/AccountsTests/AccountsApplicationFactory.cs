using System;
using System.Reflection;
using BoostRoom.Accounts.Application;
using BoostRoom.Accounts.Domain;
using BoostRoom.Accounts.Domain.ClientAggregate;
using BoostRoom.Accounts.Domain.SellerAggregate;
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
    public class AccountsApplicationFactory<TStartup> : WebApplicationFactory<TStartup>
        where TStartup : class
    {

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.AddMediatR(typeof(AccountsApplicationAssembly));

                services.AddSingleton<IPasswordEncoder, AesPasswordEncoder>();
                services.AddSingleton<IUniqueAccountsRepository, UniqueAccountsRepository>();
                services.AddSingleton<IClientRepository, ClientRepository>();
                services.AddSingleton<ISellerRepository, SellerRepository>();

                services.AddScoped<RegistrationService, RegistrationService>();
            });
        }
    }
}