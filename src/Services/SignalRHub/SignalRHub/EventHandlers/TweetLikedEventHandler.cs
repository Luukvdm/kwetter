using System.Threading.Tasks;
using Kwetter.BuildingBlocks.EventBus.EventBus.Interfaces;
using Kwetter.Services.SignalRHub.SignalRHub.Hubs;
using Kwetter.Services.Tweet.Events.Notifications;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Kwetter.Services.SignalRHub.SignalRHub.EventHandlers
{
    public class TweetLikedEventHandler : IIntegrationEventHandler<TweetLikedNotification>
    {
        private readonly IHubContext<TweetHub> _tweetHub;
        private readonly ILogger<TweetLikedEventHandler> _logger;

        public TweetLikedEventHandler(IHubContext<TweetHub> tweetHub, ILogger<TweetLikedEventHandler> logger)
        {
            _tweetHub = tweetHub;
            _logger = logger;
        }

        public async Task Handle(TweetLikedNotification notification)
        {
            _logger.LogInformation("Sending tweet liked event to the client");
            
            await _tweetHub.Clients
                .Group(notification.UserId)
                .SendAsync("TweetMessageLiked", notification);
        }
    }
}