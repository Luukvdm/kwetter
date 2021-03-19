using System;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Kwetter.Services.Core.Application.Common.Models;
using Kwetter.Services.Identity.Api.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Kwetter.Services.Identity.Api.Infrastructure.Persistence
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedDefaultUserAsync(/* IdentityDbContext<ApplicationUser> context, */UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager, ConfigurationDbContext configContext, ConfigUrls configUrls)
        {
            /*if (!configContext.Clients.Any())
            {
                foreach (var client in Config.Clients(configUrls))
                {
                    await configContext.Clients.AddAsync(client.ToEntity());
                }

                // var clientEntities = Config.Clients(configUrls).Select(e => e.ToEntity());
                // await configContext.Clients.AddRangeAsync(clientEntities);
                await configContext.SaveChangesAsync();
            }

            if (!configContext.ApiResources.Any())
            {
                var apiResEntities = Config.ApiResources.Select(e => e.ToEntity());
                await configContext.ApiResources.AddRangeAsync(apiResEntities);
                await configContext.SaveChangesAsync();
            }

            if (!configContext.IdentityResources.Any())
            {
                var idsResEntities = Config.IdentityResources.Select(e => e.ToEntity());
                await configContext.IdentityResources.AddRangeAsync(idsResEntities);
                await configContext.SaveChangesAsync();
            }

            if (!configContext.ApiScopes.Any())
            {
                var apiScopeEntities = Config.ApiScopes.Select(e => e.ToEntity());
                await configContext.ApiScopes.AddRangeAsync(apiScopeEntities);
                await configContext.SaveChangesAsync();
            }*/

            /* if (!await context.Roles.AnyAsync())
            {
                var adminRole = new IdentityRole("admin");
                var modRole = new IdentityRole("moderator");
                
                await context.Roles.AddAsync(adminRole);
                await context.AddAsync(modRole);
            } */


            var adminRole = new IdentityRole("admin");
            var modRole = new IdentityRole("moderator");

            if (await roleManager.FindByNameAsync(adminRole.Name) == null)
            {
                await roleManager.CreateAsync(adminRole);
            }

            if (await roleManager.FindByNameAsync(modRole.Name) == null)
            {
                await roleManager.CreateAsync(modRole);
            }

            var defaultAdmin = CreateDefaultApplicationUser();
            if (userManager.Users.All(u => u.Email != defaultAdmin.Email))
            {
                await userManager.CreateAsync(defaultAdmin);
                await userManager.AddToRoleAsync(defaultAdmin, adminRole.Name);
            }
        }

        private static ApplicationUser CreateDefaultApplicationUser()
        {
            var passwordHasher = new PasswordHasher<ApplicationUser>();

            var user = new ApplicationUser
            {
                Email = NormalizeString("admin@kwetter.com"),
                NormalizedEmail = NormalizeString("admin@kwetter.com"),
                Id = Guid.NewGuid().ToString(),
                UserName = NormalizeString("admin"),
                NormalizedUserName = NormalizeString("admin"),
                SecurityStamp = Guid.NewGuid().ToString("D"),
                PasswordHash = NormalizeString("@Welkom1"), // Note: This is the password
            };

            user.PasswordHash = passwordHasher.HashPassword(user, user.PasswordHash);

            return user;
        }

        private static string NormalizeString(string value)
        {
            return value.Trim('"').Trim();
        }
    }
}