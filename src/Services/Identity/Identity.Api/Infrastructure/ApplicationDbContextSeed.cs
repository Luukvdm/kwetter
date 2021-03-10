using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Kwetter.Services.Identity.Api.Infrastructure
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedDefaultUserAsync(UserManager<IdentityUser> userManager)
        {
            var defaultAdmin = new IdentityUser {UserName = "admin@energygrid.com", Email = "admin@energygrid.com"};

            if (userManager.Users.All(u => u.UserName != defaultAdmin.UserName))
            {
                await userManager.CreateAsync(defaultAdmin, "@Welkom1");
            }
        }
    }
}
