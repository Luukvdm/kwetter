using System.Threading.Tasks;
using Kwetter.BuildingBlocks.CQRS.Exceptions;
using Kwetter.BuildingBlocks.CQRS.Services;
using Kwetter.BuildingBlocks.EventBus.EventBus.Interfaces;
using Kwetter.Services.UserRelations.Application.Commands.RemoveFollow;
using Kwetter.Services.UserRelations.Events.Events;
using Kwetter.Services.UserRelations.Events.Notifications;
using MediatR;

namespace Kwetter.Services.UserRelations.Application.EventHandlers
{
    public class RemoveFollowEventHandler : IIntegrationEventHandler<RemoveFollowEvent>
    {
        private readonly ISender _mediator;
        private readonly IEventBus _eventBus;
        private readonly IExceptionHandler _exceptionHandler;

        public RemoveFollowEventHandler(ISender mediator, IEventBus eventBus)
        {
            _mediator = mediator;
            _eventBus = eventBus;
        }

        public async Task Handle(RemoveFollowEvent @event)
        {
            try
            {
                await _mediator.Send(new RemoveFollowCommand(@event.FollowingUserId, @event.FollowedUserId));
                _eventBus.Publish(new FollowerRemovedNotification(@event.FollowedUserId));
            }
            catch (ValidationException validationException)
            {
                _exceptionHandler?.HandleValidationException(validationException, @event.FollowingUserId);
            }
        }
    }
}