using System;
using System.Threading;
using System.Threading.Tasks;
using Kwetter.Services.Core.Tweet.Application.Common.Interfaces;
using Kwetter.Services.Tweet.Domain.Entities;
using MediatR;

namespace Kwetter.Services.Core.Tweet.Application.Commands.CreateTweetMessage
{
    public class CreateTweetMessageCommand : IRequest<TweetMessage>
    {
        public CreateTweetMessageCommand(string message, string posterId, DateTime postTime)
        {
            Message = message;
            PosterId = posterId;
            PostTime = postTime;
        }
        public string Message { get; set; }
        public string PosterId { get; set; }
        public DateTime PostTime { get; set; }
    }

    public class CreateTweetMessageCommandHandler : IRequestHandler<CreateTweetMessageCommand, TweetMessage>
    {
        private readonly IApplicationDbContext _context;

        public CreateTweetMessageCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TweetMessage> Handle(CreateTweetMessageCommand request, CancellationToken cancellationToken)
        {
            var entity = new TweetMessage(request.Message, request.PosterId, request.PostTime);
            
            _context.TweetMessages.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
            
            return entity;
        }
    }
}