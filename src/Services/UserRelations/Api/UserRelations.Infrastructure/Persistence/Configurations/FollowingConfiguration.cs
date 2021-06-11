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

            builder.Ignore(e => e.DomainEvents);
        }
    }
}