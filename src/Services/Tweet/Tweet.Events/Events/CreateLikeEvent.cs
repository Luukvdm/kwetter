using System;
using Kwetter.BuildingBlocks.EventBus.EventBus.Events;

namespace Kwetter.Services.Tweet.Events.Events
{
    public record CreateLikeEvent : IntegrationEvent
    {
        public CreateLikeEvent(int tweetId, string likerId, DateTime likeTime)
        {
            TweetId = tweetId;
            LikerId = likerId;
            LikeTime = likeTime;
        }

        public int TweetId { get; set; }
        public string LikerId { get; set; }
        public DateTime LikeTime { get; set; }
    }
}