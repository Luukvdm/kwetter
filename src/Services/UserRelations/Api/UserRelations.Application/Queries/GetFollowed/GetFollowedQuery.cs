using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Kwetter.Services.UserRelations.Application.Common.Interfaces;
using Kwetter.Services.UserRelations.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kwetter.Services.UserRelations.Application.Queries.GetFollowed
{
    public class GetFollowedQuery : IRequest<IList<Following>>
    {
        public GetFollowedQuery(string userId)
        {
            UserId = userId;
        }

        public string UserId { get; set; }
    }

    public class GetFollowedQueryHandler : IRequestHandler<GetFollowedQuery, IList<Following>>
    {
        private readonly IApplicationDbContext _context;

        public GetFollowedQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IList<Following>> Handle(GetFollowedQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.Followings
                .Where(e => e.FollowingUserId == request.UserId)
                .ToListAsync(cancellationToken);
            return result;
        }
    }
}