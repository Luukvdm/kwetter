using GeneralEvents.Notifications;
using Kwetter.BuildingBlocks.CQRS.Exceptions;
using Kwetter.BuildingBlocks.CQRS.Services;
using Kwetter.BuildingBlocks.EventBus.EventBus.Interfaces;

namespace Kwetter.Services.UserRelations.Infrastucture.Services
{
    public class ExceptionHandler : IExceptionHandler
    {
        private readonly IEventBus _eventBus;

        public ExceptionHandler(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public void HandleValidationException(ValidationException validationException, string userId)
        {
            // This looks dramatic but most of the time its just one notification
            foreach (var error in validationException.Errors)
            {
                foreach (string message in error.Value)
                {
                    _eventBus.Publish(new FailureNotification(message, userId));
                }
            }
        }
    }
}