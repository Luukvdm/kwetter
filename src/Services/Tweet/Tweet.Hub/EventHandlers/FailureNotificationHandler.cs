using System;
using System.Threading.Tasks;
using Kwetter.BuildingBlocks.EventBus.EventBus.Interfaces;
using Kwetter.Services.Tweet.Events.Notifications;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Kwetter.Services.Tweet.Hub.EventHandlers
{
    public class FailureNotificationHandler : IIntegrationEventHandler<FailureNotification>
    {
        private readonly IHubContext<TweetHub> _tweetHub;
        private readonly ILogger<FailureNotificationHandler> _logger;

        public FailureNotificationHandler(IHubContext<TweetHub> tweetHub, ILogger<FailureNotificationHandler> logger)
        {
            _tweetHub = tweetHub;
            _logger = logger;
        }

        public async Task Handle(FailureNotification notification)
        {
            _logger.LogInformation("Sending failure notification to the client: {clientId}", notification.UserId);
            Console.WriteLine(notification.Message);

            await _tweetHub.Clients
                .Group(notification.UserId)
                .SendAsync("FailureNotification", new {Message = notification.Message}); 
        }
    }
}