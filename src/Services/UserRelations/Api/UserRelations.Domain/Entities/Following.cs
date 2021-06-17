using System;
using Kwetter.BuildingBlocks.KwetterDomain;

namespace Kwetter.Services.UserRelations.Domain.Entities
{
    public class Following : BaseEntity
    {
        private Following() { }

        public Following(string followingUserId, string followedUserId, DateTime followedSince)
        {
            FollowingUserId = followingUserId;
            FollowedUserId = followedUserId;
            FollowedSince = followedSince;
        }
        public string FollowingUserId { get; private set; }
        public string FollowedUserId { get; private set; }
        public DateTime FollowedSince { get; private set; }
    }
}