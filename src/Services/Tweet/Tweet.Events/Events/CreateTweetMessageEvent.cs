using System;
using Kwetter.BuildingBlocks.EventBus.EventBus.Events;

namespace Kwetter.Services.Tweet.Events.Events
{
    public record CreateTweetMessageEvent: IntegrationEvent
    {
        public string Message { get; }
        public string CreatorId { get; }
        public DateTime PostTime { get; }

        public CreateTweetMessageEvent(string message, string creatorId, DateTime postTime)
        {
            Message = message;
            CreatorId = creatorId;
            PostTime = postTime;
        }
    }
}