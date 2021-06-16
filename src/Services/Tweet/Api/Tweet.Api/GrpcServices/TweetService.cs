using System.Threading.Tasks;
using Kwetter.BuildingBlocks.Abstractions.Services;
using Kwetter.BuildingBlocks.EventBus.EventBus.Interfaces;
using Kwetter.Services.Tweet.Application.Queries.GetTweetMessageQuery;
using Kwetter.Services.Tweet.Events.Events;
using Kwetter.Services.Tweet.GrpcContracts;
using Kwetter.Services.Tweet.GrpcContracts.Models;
using MediatR;

namespace Kwetter.Services.Tweet.Api.GrpcServices
{
    public class TweetService : ITweetService
    {
        private readonly IEventBus _eventBus;
        private readonly ISender _mediator;
        private readonly IDateTime _dateTime;

        public TweetService(IEventBus eventBus, ISender mediator, IDateTime dateTime)
        {
            _eventBus = eventBus;
            _mediator = mediator;
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

        public ValueTask LikeTweet(CreateLike like)
        {
            _eventBus.Publish(new CreateLikeEvent(like.TweetId, like.UserId, like.LikeTime));
            return ValueTask.CompletedTask;
        }
    }
}