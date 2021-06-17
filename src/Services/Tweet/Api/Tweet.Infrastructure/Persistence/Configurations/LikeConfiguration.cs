using Kwetter.Services.Tweet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kwetter.Services.Tweet.Infrastructure.Persistence.Configurations
{
    public class LikeConfiguration : IEntityTypeConfiguration<Like>
    {
        public void Configure(EntityTypeBuilder<Like> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasIndex(e => e.UserId);

            builder.HasOne(e => e.TweetMessage)
                .WithMany(m => m.Likes);

            builder.Ignore(e => e.DomainEvents);
        }
    }
}