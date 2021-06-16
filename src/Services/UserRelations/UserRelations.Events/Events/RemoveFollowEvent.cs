using Kwetter.BuildingBlocks.EventBus.EventBus.Events;

namespace Kwetter.Services.UserRelations.Events.Events
{
    public record RemoveFollowEvent : IntegrationEvent
    {
        public RemoveFollowEvent(string followingUserId, string followedUserId)
        {
            FollowingUserId = followingUserId;
            FollowedUserId = followedUserId;
        }

        public string FollowingUserId { get; set; }
        public string FollowedUserId { get; set; }
    }
}