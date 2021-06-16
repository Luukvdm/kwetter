using System;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Kwetter.BuildingBlocks.Configurations.Models;
using Kwetter.BuildingBlocks.IdentityBlocks.Constants;
using Kwetter.Services.Identity.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Kwetter.Services.Identity.Api.Infrastructure.Persistence
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedDefaultUserAsync(/* IdentityDbContext<ApplicationUser> context, */UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager, ConfigurationDbContext configContext, UrlConfig urlConfig)
        {
            // Create all identity clients in database
            foreach (var client in Config.Clients(urlConfig))
            {
                if (await configContext.Clients.AnyAsync(e => e.ClientId == client.ClientId)) continue;
                await configContext.Clients.AddAsync(client.ToEntity());
                await configContext.SaveChangesAsync();
            }

            // Create roles
            foreach (string roleString in Roles.ToList())
            {
                if (await roleManager.RoleExistsAsync(roleString)) continue;
                var idRole = new IdentityRole(roleString);
                await roleManager.CreateAsync(idRole);
            }

            // Refresh all identity resources
            configContext.IdentityResources.RemoveRange(await configContext.IdentityResources.ToListAsync());
            await configContext.SaveChangesAsync();
            var resources = Config.IdentityResources.Select(e => e.ToEntity());
            configContext.IdentityResources.AddRange(resources);
            await configContext.SaveChangesAsync(default);

            // Refresh all api scopes
            configContext.ApiScopes.RemoveRange(await configContext.ApiScopes.ToListAsync());
            await configContext.SaveChangesAsync();
            configContext.ApiScopes.AddRange(Config.ApiScopes.Select(e => e.ToEntity()));
            await configContext.SaveChangesAsync(default);
            
            // Refresh all api resources
            configContext.ApiResources.RemoveRange(await configContext.ApiResources.ToListAsync());
            await configContext.SaveChangesAsync();
            configContext.ApiResources.AddRange(Config.ApiResources.Select(e => e.ToEntity()));
            await configContext.SaveChangesAsync(default);

            // Add a admin user to test with
            var defaultAdmin = new ApplicationUser {UserName = "admin", DisplayName = "Je Favoriete Admin", Email = "admin@kwetter.com"};
            var testUser1 = new ApplicationUser { UserName = "user1", DisplayName = "User #1", Email = "user1@kwetter.com" };
            var testUser2 = new ApplicationUser { UserName = "user2", DisplayName = "User #2", Email = "user2@kwetter.com" };
            var testUser3 = new ApplicationUser { UserName = "user3", DisplayName = "User #3", Email = "user3@kwetter.com" };

            if (userManager.Users.All(u => u.UserName != defaultAdmin.UserName))
            {
                await userManager.CreateAsync(defaultAdmin, "@Welkom1");
                await userManager.AddToRoleAsync(defaultAdmin, Roles.Admin);

                await userManager.CreateAsync(testUser1, "@Welkom1");
                await userManager.CreateAsync(testUser2, "@Welkom1");
                await userManager.CreateAsync(testUser3, "@Welkom1");

                Console.WriteLine("User id: " + defaultAdmin.Id);
            }
        }
    }
}