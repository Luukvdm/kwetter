﻿using IdentityServer4.Services;
using Kwetter.Services.Identity.Api.Infrastructure.Persistence;
using Kwetter.Services.Identity.Api.Models;
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
            if (configuration.GetValue<bool>("ApplicationDb:UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("IdentityServerDb"));
            }
            else
            {
                string connectionString = configuration.GetConnectionString("ApplicationDbConnection");

                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseNpgsql(connectionString,
                        b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            }

            // services.AddScoped<ApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

            return services;
        }

        public static IServiceCollection AddKwetterIdentityServer(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddTransient<ILoginService<ApplicationUser>, LoginService>();
            services.AddTransient<IRedirectService, RedirectService>();

            /* services.AddAuthentication(o =>
                {
                    o.DefaultScheme = IdentityConstants.ApplicationScheme;
                    o.DefaultSignInScheme = IdentityConstants.ExternalScheme;
                })
                .AddIdentityServerJwt()
                .AddIdentityCookies(o => { }); */

            services.AddIdentity<ApplicationUser, IdentityRole>(o => { o.Stores.MaxLengthForKeys = 128; })
                // .AddDefaultUI()
                .AddDefaultTokenProviders()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();


            /*services.AddDefaultIdentity<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();*/

            services.AddIdentityServer()
                // .AddApiAuthorization<ApplicationUser, ApplicationDbContext>()
                .AddAspNetIdentity<ApplicationUser>()
                // .AddOperationalStore<ApplicationDbContext>()
                // .ConfigureReplacedServices()
                .AddIdentityResources()
                //.AddApiResources()
                //.AddClients()
                .AddSigningCredentials()
                .AddOperationalStore(options =>
                {
                    if (configuration.GetValue<bool>("ApplicationDb:UseInMemoryDatabase"))
                    {
                        options.ConfigureDbContext = builder => builder.UseInMemoryDatabase("IdentityServerDb");
                    }
                    else
                    {
                        string connectionString = configuration.GetConnectionString("ApplicationDbConnection");

                        options.ConfigureDbContext = builder => builder.UseNpgsql(connectionString,
                            b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
                    }
                })
                .AddConfigurationStore(options =>
                {
                    string connectionString = configuration.GetConnectionString("ConfigurationDbConnection");
                    
                    if (configuration.GetValue<bool>("ConfigurationDb:UseInMemoryDatabase"))
                        options.ConfigureDbContext = builder => builder.UseInMemoryDatabase("IdentityServerConfDb");
                    else
                        options.ConfigureDbContext = builder => builder.UseNpgsql(connectionString,
                            b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
                })
                .Services.AddTransient<IProfileService, ProfileService>();

            services.AddAuthentication()
                .AddIdentityServerJwt();

            return services;
        }
    }
}