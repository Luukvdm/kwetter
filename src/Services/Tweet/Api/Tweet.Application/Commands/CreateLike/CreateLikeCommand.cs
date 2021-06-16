using System;
using System.Threading;
using System.Threading.Tasks;
using Kwetter.Services.Tweet.Application.Common.Interfaces;
using Kwetter.Services.Tweet.Domain.Entities;
using MediatR;

namespace Kwetter.Services.Tweet.Application.Commands.CreateLike
{
    public class CreateLikeCommand : IRequest<Like>
    {
        public CreateLikeCommand(string userId, DateTime postTime, int tweetMessageId)
        {
            UserId = userId;
            PostTime = postTime;
            TweetMessageId = tweetMessageId;
        }

        public string UserId { get; set; }
        public DateTime PostTime { get; }
        public int TweetMessageId { get; set; }
    }

    public class CreateLikeCommandHandler : IRequestHandler<CreateLikeCommand, Like>
    {
        private readonly IApplicationDbContext _context;

        public CreateLikeCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Like> Handle(CreateLikeCommand request, CancellationToken cancellationToken)
        {
            var entity = new Like(request.UserId, request.PostTime, request.TweetMessageId);
            _context.Likes.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return entity;
        }
    }
}