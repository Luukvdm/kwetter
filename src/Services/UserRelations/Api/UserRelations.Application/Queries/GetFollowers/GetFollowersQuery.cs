using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Kwetter.Services.UserRelations.Application.Common.Interfaces;
using Kwetter.Services.UserRelations.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kwetter.Services.UserRelations.Application.Queries.GetFollowers
{
    public class GetFollowersQuery : IRequest<IList<Following>>
    {
        public GetFollowersQuery(string userId)
        {
            UserId = userId;
        }
        
        public string UserId { get; set; }
    }
    
    public class GetFollowersQueryHandler : IRequestHandler<GetFollowersQuery, IList<Following>>
    {
        private readonly IApplicationDbContext _context;

        public GetFollowersQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IList<Following>> Handle(GetFollowersQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.Followings
                .Where(e => e.FollowedUserId == request.UserId)
                .ToListAsync(cancellationToken);
            return result;
        }
    }
}