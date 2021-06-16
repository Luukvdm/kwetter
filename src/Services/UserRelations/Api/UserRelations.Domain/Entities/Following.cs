using System;
using Kwetter.BuildingBlocks.KwetterDomain;

namespace Kwetter.Services.UserRelations.Domain.Entities
{
    public class Following : BaseEntity
    {
        private Following()
        {
        }

        public Following(string followingUserId, string followedUserId, DateTime followedSince)
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