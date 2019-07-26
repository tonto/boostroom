using BoostRoom.Accounts.Application;
using BoostRoom.Accounts.Domain;
using BoostRoom.Accounts.Domain.ClientAggregate;
using BoostRoom.Infrastructure;
using BoostRoom.Infrastructure.Accounts;
using BoostRoom.Infrastructure.Accounts.RavenDB;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Raven.Client.Documents;
using IEventStore = Tactical.DDD.EventSourcing.IEventStore;

namespace BoostRoom.WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private static UniqueAccountsProjection _uniqueAccountsProjection;

        public void ConfigureServices(IServiceCollection services)
        {
            // Application dependencies
            
            services.AddMediatR(typeof(AccountsApplicationAssembly));

            services.AddSingleton<IPasswordEncoder, AesPasswordEncoder>();
            services.AddSingleton<IUniqueAccountsRepository, UniqueAccountsRepository>();
            services.AddSingleton<IClientsRepository, ClientsRepository>();

            services.AddScoped<RegistrationService, RegistrationService>();

            var esSection = Configuration.GetSection("EventStore");
            
            var esConnection = BoostRoomEventStoreConnection.ConnectAsync(
                esSection["Host"],
                esSection["Username"],
                esSection["Password"]
                ).GetAwaiter().GetResult();
            
            var eventStore = new BoostRoom.Infrastructure.EventStore(esConnection);

            services.AddSingleton<IEventStore>(eventStore);
            services.AddSingleton<BoostRoom.Infrastructure.IEventStore>(eventStore);

            var rdbSection = Configuration.GetSection("RavenDB");
            
            var store = BoostRoomDocumentStore.Create(rdbSection["Host"], rdbSection["Database"]); 

            services.AddSingleton<IDocumentStore>(store);
            
            _uniqueAccountsProjection = new UniqueAccountsProjection(store, eventStore);

            // MVC Dependencies
            
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerGen(c =>
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "Boostroom API", Version = "v1"})
            );

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration => { configuration.RootPath = "ClientApp/dist"; });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//                app.UseHsts();
            }

//            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSwagger();

            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Boostroom API v1"); });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}