using System;
using Kwetter.BuildingBlocks.EventBus.EventBus.Events;

namespace Kwetter.Services.Tweet.Events.Notifications
{
    public record TweetLikedNotification : IntegrationEvent
    {
        public TweetLikedNotification(string userId, DateTime postTime, int tweetMessageId)
        {
            UserId = userId;
            PostTime = postTime;
            TweetMessageId = tweetMessageId;
        }

        public string UserId { get; set; }
        public DateTime PostTime { get; }
        public int TweetMessageId { get; set; }
    }

    public record TweetLikedFailedNotification : IntegrationEvent
    {
        public TweetLikedFailedNotification(int messageId, string userId)
        {
            MessageId = messageId;
            UserId = userId;
        }

        public int MessageId { get; set; }
        public string UserId { get; set; }
    }
}