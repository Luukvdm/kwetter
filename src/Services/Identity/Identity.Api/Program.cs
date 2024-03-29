﻿using System;
using System.Threading.Tasks;
using IdentityServer4.EntityFramework.DbContexts;
using Kwetter.BuildingBlocks.Configurations.Extensions;
using Kwetter.BuildingBlocks.Configurations.Models;
using Kwetter.BuildingBlocks.KwetterLogger;
using Kwetter.Services.Identity.Api.Infrastructure.Persistence;
using Kwetter.Services.Identity.Api.Models;
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
                    var context = services.GetRequiredService<ApplicationDbContext>();
                    var configContext = services.GetRequiredService<ConfigurationDbContext>();
                    var grantsContext = services.GetRequiredService<PersistedGrantDbContext>();

                    if (!context.Database.IsInMemory())
                    {
                        await context.Database.MigrateAsync();
                    }

                    if (!configContext.Database.IsInMemory())
                    {
                        await configContext.Database.MigrateAsync();
                    }

                    if (!grantsContext.Database.IsInMemory())
                    {
                        await grantsContext.Database.MigrateAsync();
                    }

                    // Seed the database with a default account
                    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                    var configUrls = services.GetRequiredService<UrlConfig>();
                    await ApplicationDbContextSeed.SeedDefaultUserAsync(userManager, roleManager, configContext, configUrls);
                }
                catch (Exception ex)
                {
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while migrating or seeding the database(s).");
                    throw;
                }
            }

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .UseKwetterLogger()
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddSharedJson(hostingContext.HostingEnvironment);
                    config.AddKwetterLoggerConfiguration(hostingContext.HostingEnvironment);
                    config.AddEnvironmentVariables();
                    config.AddCommandLine(args);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        }
    }
}
