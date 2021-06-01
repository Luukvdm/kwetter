using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Kwetter.BuildingBlocks.Abstractions.Services;
using Kwetter.BuildingBlocks.Core.Domain.Common;
using Kwetter.Services.Core.Tweet.Application.Common.Interfaces;
using Kwetter.Services.Tweet.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kwetter.Services.Tweet.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTime _dateTime;
        
        public ApplicationDbContext(DbContextOptions options, ICurrentUserService currentUserService,
            IDateTime dateTime) : base(options) 
        {
            _currentUserService = currentUserService;
            _dateTime = dateTime;
        }

        public DbSet<TweetMessage> TweetMessages { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Like> Likes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
        
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _currentUserService.UserId;
                        entry.Entity.Created = _dateTime.Now;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = _currentUserService.UserId;
                        entry.Entity.LastModified = _dateTime.Now;
                        break;
                }
            }

            int result = await base.SaveChangesAsync(cancellationToken);

            return result;
        }
    }
}
