using System;
using Kwetter.Services.Identity.Api.Infrastructure;
using Kwetter.Services.Identity.Api.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kwetter.Services.Identity.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                Console.WriteLine("Using in memory DB");
                services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("IdentityServerDb"));
            }
            else
            {
                Console.WriteLine("Using ProductionConnection string");
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                        b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            }

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

            return services;
        }

        public static IServiceCollection AddIdentityServer(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDefaultIdentity<IdentityUser>().AddEntityFrameworkStores<ApplicationDbContext>();

            // Get the clients from appsettings.json
            var clients = configuration.GetSection("IdentityServer:Oid-Clients");
            services.AddIdentityServer()
                .AddApiAuthorization<IdentityUser, ApplicationDbContext>()
                .AddInMemoryClients(clients)
                .AddInMemoryApiScopes(Config.ApiScopes)
                .AddInMemoryApiResources(Config.ApiResources);

            services.AddAuthentication()
                .AddIdentityServerJwt();

            return services;
        }
    }
}
