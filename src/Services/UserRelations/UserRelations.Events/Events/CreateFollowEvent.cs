using System;
using Kwetter.BuildingBlocks.EventBus.EventBus.Events;

namespace Kwetter.Services.UserRelations.Events.Events
{
    public record CreateFollowEvent : IntegrationEvent
    {
        public CreateFollowEvent(string followerUserId, string followedUserId, DateTime followedSince)
        {
            FollowerUserId = followerUserId;
            FollowedUserId = followedUserId;
            FollowedSince = followedSince;
        }

        public string FollowerUserId { get; set; }
        public string FollowedUserId { get; set; }
        public DateTime FollowedSince { get; set; }
    }
}