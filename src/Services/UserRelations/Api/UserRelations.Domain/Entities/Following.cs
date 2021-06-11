using System;
using Kwetter.BuildingBlocks.KwetterDomain;

namespace Kwetter.Services.UserRelations.Domain.Entities
{
    public class Following : BaseEntity
    {
        public Following(string followerUserId, string followedUserId, DateTime followedSince)
        {
            FollowerUserId = followerUserId;
            FollowedUserId = followedUserId;
            FollowedSince = followedSince;
        }
        public string FollowerUserId { get; }
        public string FollowedUserId { get; }
        public DateTime FollowedSince { get; }
    }
}