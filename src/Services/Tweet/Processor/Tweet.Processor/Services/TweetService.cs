using System.Threading.Tasks;
using Kwetter.BuildingBlocks.Abstractions.Services;
using Kwetter.BuildingBlocks.EventBus.EventBus.Interfaces;
using Kwetter.Services.Core.Tweet.Application.Queries.GetTweetMessageQuery;
using Kwetter.Services.Tweet.Events.Events;
using Kwetter.Services.Tweet.GrpcContracts;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Kwetter.Services.Tweet.Processor.Services
{
    public class TweetService : ITweetService
    {
        private readonly IEventBus _eventBus;
        private readonly ISender _mediator;
        private readonly ICurrentUserService _currentUser;
        private readonly IDateTime _dateTime;

        public TweetService(IEventBus eventBus, ISender mediator, ICurrentUserService currentUser, IDateTime dateTime)
        {
            _eventBus = eventBus;
            _mediator = mediator;
            _currentUser = currentUser;
            _dateTime = dateTime;
        }

        public ValueTask CreateTweet(CreateTweet tweet)
        {
            _eventBus.Publish(new CreateTweetMessageEvent(tweet.Message, tweet.UserId, tweet.PostTime));
            return ValueTask.CompletedTask;
        }

        public async ValueTask<ContractTweetMessage> GetTweet(string id)
        {
            var result = await _mediator.Send(new GetTweetMessageQuery(int.Parse(id)));
            var tweet = new ContractTweetMessage
            {
                Id = result.Id,
                Message = result.Message,
                PostTime = result.PostTime,
                CreatorId = result.CreatorId
            };
            return tweet;
        }
    }
}