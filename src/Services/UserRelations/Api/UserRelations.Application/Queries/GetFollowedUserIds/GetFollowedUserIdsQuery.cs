using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Kwetter.Services.UserRelations.Application.Common.Interfaces;
using Kwetter.Services.UserRelations.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kwetter.Services.UserRelations.Application.Queries.GetFollowedUserIds
{
    public class GetFollowedUserIdsQuery : IRequest<IList<Following>>
    {
        public GetFollowedUserIdsQuery(string userId)
        {
            UserId = userId;
        }

        public string UserId { get; set; }
    }

    public class GetFollowedUserIdsQueryHandler : IRequestHandler<GetFollowedUserIdsQuery, IList<Following>>
    {
        private readonly IApplicationDbContext _context;

        public GetFollowedUserIdsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IList<Following>> Handle(GetFollowedUserIdsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Followings.Where(e => e.FollowerUserId == request.UserId)
                .ToListAsync(cancellationToken);
        }
    }
}