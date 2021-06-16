using Kwetter.BuildingBlocks.EventBus.EventBus.Events;

namespace Kwetter.Services.UserRelations.Events.Notifications
{
    public record FollowerRemovedNotification : IntegrationEvent
    {
        public FollowerRemovedNotification(string removedFollowerUserId)
        {
            RemovedFollowerUserId = removedFollowerUserId;
        }

        public string RemovedFollowerUserId { get; set; }
    }
}