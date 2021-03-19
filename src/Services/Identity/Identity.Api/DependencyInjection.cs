using System;
using System.Reflection;
using IdentityServer4.AspNetIdentity;
using IdentityServer4.Services;
using Kwetter.Services.Core.Application.Common.Models;
using Kwetter.Services.Identity.Api.Infrastructure;
using Kwetter.Services.Identity.Api.Infrastructure.Identity;
using Kwetter.Services.Identity.Api.Infrastructure.Persistence;
using Kwetter.Services.Identity.Api.Interfaces;
using Kwetter.Services.Identity.Api.Services;
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
            IConfiguration configuration, ConfigUrls configUrls)
        {
            /* services.AddDefaultIdentity<ApplicationUser>()
                .AddRoles<IdentityRole>()
                // .AddClaimsPrincipalFactory<ClaimsFactory>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            // Get the clients from appsettings.json
            var clients = configuration.GetSection("IdentityServer:Oid-Clients");
            services.AddIdentityServer()
                .AddApiAuthorization<ApplicationUser, ApplicationDbContext>()
                .AddInMemoryClients(Config.Clients(configUrls))
                .AddInMemoryIdentityResources(Config.IdentityResources)
                .AddInMemoryApiScopes(Config.ApiScopes)
                .AddInMemoryApiResources(Config.ApiResources); */


            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddRoles<IdentityRole>()
                /*.AddDefaultTokenProviders()*/;

            // services.AddTransient<ILoginService<ApplicationUser>, EFLoginService>();
            // services.AddTransient<IRedirectService, RedirectService>();

            // var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            string? migrationsAssembly = typeof(ApplicationDbContext).Assembly.FullName;

                /* .AddInMemoryClients(Config.Clients(configUrls))
                .AddInMemoryIdentityResources(Config.IdentityResources)
                .AddInMemoryApiScopes(Config.ApiScopes)
                .AddInMemoryApiResources(Config.ApiResources); */

            services.AddIdentityServer()
                .AddApiAuthorization<ApplicationUser, ApplicationDbContext>()
                // .AddAspNetIdentity<ApplicationUser>()
                // .AddSigningCredentials()
                .AddInMemoryClients(Config.Clients(configUrls))
                .AddInMemoryApiScopes(Config.ApiScopes)
                .AddInMemoryApiResources(Config.ApiResources)
                /* .AddConfigurationStore(options =>
                {
                    if (configuration.GetValue<bool>("UseInMemoryDatabase"))
                    {
                        options.ConfigureDbContext = builder => builder.UseInMemoryDatabase("IdentityConfDb");
                    }
                    else
                    {
                        options.ConfigureDbContext = builder => builder.UseNpgsql(
                            configuration.GetConnectionString("DefaultConnection"),
                            b => b.MigrationsAssembly(migrationsAssembly));
                    }
                })*/
                /*.AddOperationalStore(options =>
                {
                    if (configuration.GetValue<bool>("UseInMemoryDatabase"))
                    {
                        options.ConfigureDbContext = builder => builder.UseInMemoryDatabase("IdentityOpDb");
                    }
                    else
                    {
                        options.ConfigureDbContext = builder => builder.UseNpgsql(
                            configuration.GetConnectionString("DefaultConnection"),
                            b => b.MigrationsAssembly(migrationsAssembly));
                    }
                })*/;
            
            // services.AddTransient<IProfileService, ProfileService>();

            services.AddAuthentication()
                .AddIdentityServerJwt();

            return services;
        }
    }
}