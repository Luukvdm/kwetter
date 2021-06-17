using Kwetter.Services.Tweet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kwetter.Services.Tweet.Infrastructure.Persistence.Configurations
{
    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasIndex(e => e.Name);

            builder.HasMany(e => e.Tweets)
                .WithMany(e => e.Tags);

            builder.Ignore(e => e.DomainEvents);
        }
    }
}