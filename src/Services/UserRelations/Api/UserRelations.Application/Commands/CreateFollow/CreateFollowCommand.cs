using System;
using System.Threading;
using System.Threading.Tasks;
using Kwetter.Services.UserRelations.Application.Common.Interfaces;
using Kwetter.Services.UserRelations.Domain.Entities;
using MediatR;

namespace Kwetter.Services.UserRelations.Application.Commands.CreateFollow
{
    public class CreateFollowCommand : IRequest<Following>
    {
        public CreateFollowCommand(string followingUserId, string followedUserId, DateTime followedSince)
        {
            FollowingUserId = followingUserId;
            FollowedUserId = followedUserId;
            FollowedSince = followedSince;
        }

        public string FollowingUserId { get; }
        public string FollowedUserId { get; }
        public DateTime FollowedSince { get; }
    }

    public class CreateFollowCommandHandler : IRequestHandler<CreateFollowCommand, Following>
    {
        private readonly IApplicationDbContext _context;

        public CreateFollowCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Following> Handle(CreateFollowCommand request, CancellationToken cancellationToken)
        {
            var entity = new Following(request.FollowingUserId, request.FollowedUserId, request.FollowedSince);
            _context.Followings.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return entity;
        }
    }
}