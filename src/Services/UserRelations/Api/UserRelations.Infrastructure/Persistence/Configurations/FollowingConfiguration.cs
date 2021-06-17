using Kwetter.Services.UserRelations.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kwetter.Services.UserRelations.Infrastucture.Persistence.Configurations
{
    public class FollowingConfiguration : IEntityTypeConfiguration<Following>
    {
        public void Configure(EntityTypeBuilder<Following> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasAlternateKey(e => new {e.FollowedUserId, e.FollowingUserId});

            builder.HasIndex(e => e.FollowedUserId);
            builder.HasIndex(e => e.FollowingUserId);

            builder.Ignore(e => e.DomainEvents);
        }
    }
}