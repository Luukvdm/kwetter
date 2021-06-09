using Kwetter.BuildingBlocks.EventBus.EventBus.Events;

namespace Kwetter.Services.Tweet.Events.Notifications
{
    public record FailureNotification : IntegrationEvent
    {
        public string Message { get; set; }
        public string UserId { get; set; }

        public FailureNotification(string message, string userId)
        {
            Message = message;
            UserId = userId;
        }
        
    }
}