using System.Threading.Tasks;
using Kwetter.BuildingBlocks.EventBus.EventBus.Interfaces;
using Kwetter.Services.Tweet.Events.Notifications;

namespace Kwetter.Services.Tweet.Hub.EventHandlers
{
    public class TweetLikedFailedEventHandler : IIntegrationEventHandler<TweetLikedFailedNotification>

    {
        public Task Handle(TweetLikedFailedNotification @event)
        {
            throw new System.NotImplementedException();
        }
    }
}