using System.Threading.Tasks;
using Kwetter.BuildingBlocks.EventBus.EventBus.Interfaces;
using Kwetter.Services.Tweet.Events.Notifications;

namespace Kwetter.Services.Tweet.Hub.EventHandlers
{
    public class TweetLikedEventHandler : IIntegrationEventHandler<TweetLikedNotification>
    {
        public Task Handle(TweetLikedNotification @event)
        {
            throw new System.NotImplementedException();
        }
    }
}