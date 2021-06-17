using Kwetter.Services.Tweet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kwetter.Services.Tweet.Infrastructure.Persistence.Configurations
{
    public class TweetConfiguration : IEntityTypeConfiguration<TweetMessage>
    {
        public void Configure(EntityTypeBuilder<TweetMessage> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasIndex(e => e.CreatorId);

            builder.HasMany(e => e.Tags)
                .WithMany(e => e.Tweets);

            builder.Property(e => e.PostTime)
                .IsRequired();
            builder.Property(e => e.Message)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(160);

            builder.Ignore(e => e.DomainEvents);
        }
    }
}