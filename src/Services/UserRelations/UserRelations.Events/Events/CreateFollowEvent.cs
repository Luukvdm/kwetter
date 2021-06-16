using System;
using Kwetter.BuildingBlocks.EventBus.EventBus.Events;

namespace Kwetter.Services.UserRelations.Events.Events
{
    public record CreateFollowEvent : IntegrationEvent
    {
        public CreateFollowEvent(string followingUserId, string followedUserId, DateTime followedSince)
        {
            FollowingUserId = followingUserId;
            FollowedUserId = followedUserId;
            FollowedSince = followedSince;
        }

        public string FollowingUserId { get; set; }
        public string FollowedUserId { get; set; }
        public DateTime FollowedSince { get; set; }
    }
}