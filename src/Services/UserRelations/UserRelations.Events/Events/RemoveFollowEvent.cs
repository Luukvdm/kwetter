using Kwetter.BuildingBlocks.EventBus.EventBus.Events;

namespace Kwetter.Services.UserRelations.Events.Events
{
    public record RemoveFollowEvent : IntegrationEvent
    {
        public string FollowerUserId { get; set; }
        public string FollowedUserId { get; set; }
    }
}