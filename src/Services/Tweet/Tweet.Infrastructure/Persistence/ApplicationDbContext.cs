using System.Reflection;
using Kwetter.Services.Core.Application.Common.Interfaces;
using Kwetter.Services.Core.Infrastructure.Persistence;
using Kwetter.Services.Core.Tweet.Application.Common.Interfaces;
using Kwetter.Services.Tweet.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kwetter.Services.Tweet.Infrastructure.Persistence
{
    public class ApplicationDbContext : BaseApplicationDbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions options, ICurrentUserService currentUserService,
            IDateTime dateTime) : base(options, currentUserService, dateTime)
        {
        }

        public DbSet<TweetMessage> TweetMessages { get; set; }
        public DbSet<Tag> Tags { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}
