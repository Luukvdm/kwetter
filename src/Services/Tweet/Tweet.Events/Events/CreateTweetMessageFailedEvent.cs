using Kwetter.BuildingBlocks.EventBus.EventBus.Events;

namespace Kwetter.Services.Tweet.Events.Events
{
    public record CreateTweetMessageFailedEvent : IntegrationEvent
    {
        public string Message { get; set; }
        public string UserId { get; set; }

        public CreateTweetMessageFailedEvent(string message, string userId)
        {
            Message = message;
            UserId = userId;
        }
    }
}