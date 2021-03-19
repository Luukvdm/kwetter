using System;
using System.Threading.Tasks;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Interfaces;
using Kwetter.Services.Core.Api;
using Kwetter.Services.Core.Application.Common.Models;
using Kwetter.Services.Identity.Api.Infrastructure;
using Kwetter.Services.Identity.Api.Infrastructure.Identity;
using Kwetter.Services.Identity.Api.Infrastructure.Persistence;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Kwetter.Services.Identity.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    /* var context = services.GetRequiredService<ApplicationDbContext>(); */
                    //var configContext = services.GetRequiredService<ConfigurationDbContext>();
                    // var opContext = services.GetRequiredService<PersistedGrantDbContext>();

                    /* if (context.Database.IsNpgsql())
                    {
                        await context.Database.MigrateAsync();
                        await configContext.Database.MigrateAsync();
                        await opContext.Database.MigrateAsync();
                    } */
                    
                    // Seed the database with a default account
                    var configUrls = services.GetRequiredService<ConfigUrls>();
                    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

                    await ApplicationDbContextSeed.SeedDefaultUserAsync(userManager, roleManager, null, configUrls);
                }
                catch (Exception ex)
                {
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while migrating or seeding the database.");
                    throw;
                }
            }

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .AddLogger()
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddSharedJson(hostingContext.HostingEnvironment);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        }
    }
}
