using System.Threading.Tasks;
using Kwetter.BuildingBlocks.CQRS.Exceptions;
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
                // This looks dramatic but most of the time its just one notification
                foreach (var error in validationException.Errors)
                {
                    foreach (string message in error.Value)
                    {
                        _eventBus.Publish(new FailureNotification(message, @event.FollowingUserId));
                    }
                }
            }
        }
    }
}