using System.Threading;
using System.Threading.Tasks;
using Kwetter.Services.UserRelations.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kwetter.Services.UserRelations.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        public DbSet<Following> Followings { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}