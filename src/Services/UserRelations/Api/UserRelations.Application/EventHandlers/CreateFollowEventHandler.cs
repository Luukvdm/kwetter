using System.Threading.Tasks;
using Kwetter.BuildingBlocks.CQRS.Exceptions;
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
        private readonly ILogger<CreateFollowEventHandler> _logger;

        public CreateFollowEventHandler(ISender mediator, IEventBus eventBus, ILogger<CreateFollowEventHandler> logger)
        {
            _mediator = mediator;
            _eventBus = eventBus;
            _logger = logger;
        }

        public async Task Handle(CreateFollowEvent @event)
        {
            try
            {
                var entity = await _mediator.Send(new CreateFollowCommand(@event.FollowedUserId, @event.FollowerUserId, @event.CreationDate));
                _eventBus.Publish(new FollowCreatedNotification(entity.FollowedUserId));
            }
            catch (ValidationException validationException)
            {
                // This looks dramatic but most of the time its just one notification
                foreach (var error in validationException.Errors)
                {
                    foreach (string message in error.Value)
                    {
                        _eventBus.Publish(new FailureNotification(message, @event.FollowerUserId));
                    }
                }
            }
        }
    }
}