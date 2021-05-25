using System.Threading.Tasks;
using Kwetter.BuildingBlocks.EventBus.EventBus.Interfaces;
using Kwetter.Services.Tweet.Events.Events;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Kwetter.Services.Tweet.Hub.EventHandlers
{
    public class CreateTweetMessageFailedEventHandler : IIntegrationEventHandler<CreateTweetMessageFailedEvent>
    {
        private readonly IHubContext<TweetHub> _tweetHub;
        private readonly ILogger<CreateTweetMessageFailedEvent> _logger;

        public CreateTweetMessageFailedEventHandler(IHubContext<TweetHub> tweetHub,
            ILogger<CreateTweetMessageFailedEvent> logger)
        {
            _tweetHub = tweetHub;
            _logger = logger;
        }

        public async Task Handle(CreateTweetMessageFailedEvent @event)
        {
            _logger.LogInformation("Sending validationException to the client");

            await _tweetHub.Clients
                .Group(@event.UserId)
                .SendAsync("CreateTweetMessageFailure", new {Message = @event.Message});
        }
    }
}