using Kwetter.BuildingBlocks.EventBus.EventBus.Events;

namespace Kwetter.Services.UserRelations.Events.Notifications
{
    public record FollowCreatedNotification : IntegrationEvent
    {
        public FollowCreatedNotification(string followedUserId)
        {
            FollowedUserId = followedUserId;
        }

        public string FollowedUserId { get; }
    }
}