using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Kwetter.BuildingBlocks.Abstractions.Exceptions;
using Kwetter.Services.UserRelations.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kwetter.Services.UserRelations.Application.Commands.RemoveFollow
{
    public class RemoveFollowCommand : IRequest<Unit>
    {
        public RemoveFollowCommand(string followerUserId, string followedUserId)
        {
            FollowerUserId = followerUserId;
            FollowedUserId = followedUserId;
        }

        public string FollowerUserId { get; set; }
        public string FollowedUserId { get; set; }
    }

    public class RemoveFollowCommandHandler : IRequestHandler<RemoveFollowCommand, Unit>
    {
        private readonly IApplicationDbContext _context;

        public RemoveFollowCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(RemoveFollowCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Followings.Where(e => e.FollowedUserId == request.FollowedUserId)
                .SingleOrDefaultAsync(cancellationToken);

            if (entity == null) throw new NotFoundException("This person isn't being followed right now");
            
            _context.Followings.Remove(entity);
            return Unit.Value;
        }
    }
}