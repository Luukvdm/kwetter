using System;
using Kwetter.BuildingBlocks.KwetterDomain;

namespace Kwetter.Services.Tweet.Domain.Entities
{
    public class Like : BaseEntity
    {
        private Like() { }

        public Like(string userId, DateTime postTime, int tweetMessageId)
        {
            UserId = userId;
            PostTime = postTime;
            TweetMessageId = tweetMessageId;
        }

        public Like(string userId, DateTime postTime, TweetMessage tweetMessage)
        {
            UserId = userId;
            PostTime = postTime;
            TweetMessage = tweetMessage;
        }

        public string UserId { get; private set; }
        public DateTime PostTime { get; private set; }
        public int TweetMessageId { get; private set; }
        public TweetMessage TweetMessage { get; private set; }
    }
}