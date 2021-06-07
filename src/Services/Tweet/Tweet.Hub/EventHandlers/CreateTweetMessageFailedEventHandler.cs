using System.Threading.Tasks;
using Kwetter.BuildingBlocks.EventBus.EventBus.Interfaces;
using Kwetter.Services.Tweet.Events.Notifications;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Kwetter.Services.Tweet.Hub.EventHandlers
{
    public class CreateTweetMessageFailedEventHandler : IIntegrationEventHandler<CreateTweetMessageFailedNotification>
    {
        private readonly IHubContext<TweetHub> _tweetHub;
        private readonly ILogger<CreateTweetMessageFailedNotification> _logger;

        public CreateTweetMessageFailedEventHandler(IHubContext<TweetHub> tweetHub,
            ILogger<CreateTweetMessageFailedNotification> logger)
        {
            _tweetHub = tweetHub;
            _logger = logger;
        }

        public async Task Handle(CreateTweetMessageFailedNotification notification)
        {
            _logger.LogInformation("Sending validationException to the client");

            await _tweetHub.Clients
                .Group(notification.UserId)
                .SendAsync("CreateTweetMessageFailure", new {Message = notification.Message});
        }
    }
}