using System.Threading.Tasks;
using Kwetter.BuildingBlocks.EventBus.EventBus.Interfaces;
using Kwetter.Services.SignalRHub.SignalRHub.Hubs;
using Kwetter.Services.Tweet.Events.Notifications;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Kwetter.Services.SignalRHub.SignalRHub.EventHandlers
{
    public class TweetCreatedNotificationHandler : IIntegrationEventHandler<TweetCreatedNotification>
    {
        private readonly IHubContext<TweetHub> _tweetHub;
        private readonly ILogger<TweetCreatedNotificationHandler> _logger;

        public TweetCreatedNotificationHandler(IHubContext<TweetHub> tweetHub, ILogger<TweetCreatedNotificationHandler> logger)
        {
            _tweetHub = tweetHub;
            _logger = logger;
        }

        public async Task Handle(TweetCreatedNotification @event)
        {
            _logger.LogInformation("Sending tweet created notification to the client");
            
            await _tweetHub.Clients
                    // TODO Add friends and stuff
                .Group(@event.CreatorId)
                .SendAsync("TweetMessageCreated", @event);
        }
    }
}