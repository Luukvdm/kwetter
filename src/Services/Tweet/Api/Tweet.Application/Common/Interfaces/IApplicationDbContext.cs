using System.Threading;
using System.Threading.Tasks;
using Kwetter.Services.Tweet.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kwetter.Services.Tweet.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<TweetMessage> TweetMessages { get; set; }
        DbSet<Tag> Tags { get; set; }
        DbSet<Like> Likes { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}