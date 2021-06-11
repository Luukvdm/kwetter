using Kwetter.BuildingBlocks.EventBus.EventBus.Events;

namespace Kwetter.Services.UserRelations.Events.Notifications
{
    public record FailureNotification : IntegrationEvent
    {
        public FailureNotification(string message, string userId)
        {
            Message = message;
            UserId = userId;
        }

        public string Message { get; set; }
        public string UserId { get; set; }
    }
}