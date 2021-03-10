using System;
using System.Linq;
using System.Threading.Tasks;
using Kwetter.Services.Tweet.Domain.Entities;

namespace Kwetter.Services.Tweet.Infrastructure.Persistence
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedTestDataAsync(ApplicationDbContext context)
        {
            if (!context.TweetMessages.Any())
            {
                var tweet = new TweetMessage("Hello World!", DateTime.Now);
                await context.TweetMessages.AddAsync(tweet);
                await context.SaveChangesAsync();
            }
        }
    }
}
