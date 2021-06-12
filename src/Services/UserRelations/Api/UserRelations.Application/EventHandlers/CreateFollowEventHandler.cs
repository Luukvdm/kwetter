using System.Threading.Tasks;
using Kwetter.BuildingBlocks.CQRS.Exceptions;
using Kwetter.BuildingBlocks.CQRS.Services;
using Kwetter.BuildingBlocks.EventBus.EventBus.Interfaces;
using Kwetter.Services.UserRelations.Application.Commands.CreateFollow;
using Kwetter.Services.UserRelations.Events.Events;
using Kwetter.Services.UserRelations.Events.Notifications;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Kwetter.Services.UserRelations.Application.EventHandlers
{
    public class CreateFollowEventHandler : IIntegrationEventHandler<CreateFollowEvent>
    {
        private readonly ISender _mediator;
        private readonly IEventBus _eventBus;
        private readonly IExceptionHandler _exceptionHandler;
        private readonly ILogger<CreateFollowEventHandler> _logger;

        public CreateFollowEventHandler(ISender mediator, IEventBus eventBus, ILogger<CreateFollowEventHandler> logger, IExceptionHandler exceptionHandler)
        {
            _mediator = mediator;
            _eventBus = eventBus;
            _logger = logger;
            _exceptionHandler = exceptionHandler;
        }

        public async Task Handle(CreateFollowEvent @event)
        {
            try
            {
                var entity = await _mediator.Send(
                    new CreateFollowCommand(@event.FollowingUserId, @event.FollowedUserId, @event.CreationDate)
                );
                _eventBus.Publish(new FollowCreatedNotification(entity.FollowedUserId));
            }
            catch (ValidationException validationException)
            {
                _exceptionHandler?.HandleValidationException(validationException, @event.FollowingUserId);
            }
        }
    }
}