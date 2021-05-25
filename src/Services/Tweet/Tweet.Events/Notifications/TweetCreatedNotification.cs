using System;
using Kwetter.BuildingBlocks.EventBus.EventBus.Events;

namespace Kwetter.Services.Tweet.Events.Notifications
{
    public record TweetCreatedNotification : IntegrationEvent
    {
        public TweetCreatedNotification(int id, string message, string creatorId, DateTime postTime)
        {
            Id = id;
            Message = message;
            CreatorId = creatorId;
            PostTime = postTime;
        }

        public int Id { get; }
        public string Message { get; }
        public string CreatorId { get; }
        public DateTime PostTime { get; }
    }
}