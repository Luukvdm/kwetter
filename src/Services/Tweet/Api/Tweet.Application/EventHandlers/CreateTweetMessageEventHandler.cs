using System;
using System.Threading.Tasks;
using Kwetter.BuildingBlocks.CQRS.Exceptions;
using Kwetter.BuildingBlocks.CQRS.Services;
using Kwetter.BuildingBlocks.EventBus.EventBus.Interfaces;
using Kwetter.Services.Core.Tweet.Application.Commands.CreateTweetMessage;
using Kwetter.Services.Tweet.Events.Events;
using Kwetter.Services.Tweet.Events.Notifications;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Kwetter.Services.Core.Tweet.Application.EventHandlers
{
    public class CreateTweetMessageEventHandler : IIntegrationEventHandler<CreateTweetMessageEvent>
    {
        private readonly ISender _mediator;
        private readonly IEventBus _eventBus;
        private readonly IExceptionHandler _exceptionHandler;
        private readonly ILogger<CreateTweetMessageEventHandler> _logger;

        public CreateTweetMessageEventHandler(ISender mediator, IEventBus eventBus,
            ILogger<CreateTweetMessageEventHandler> logger, IExceptionHandler exceptionHandler)
        {
            _mediator = mediator;
            _eventBus = eventBus;
            _logger = logger;
            _exceptionHandler = exceptionHandler;
        }

        public async Task Handle(CreateTweetMessageEvent @event)
        {
            try
            {
                var entity =
                    await _mediator.Send(new CreateTweetMessageCommand(@event.Message, @event.CreatorId,
                        @event.PostTime));
                _eventBus.Publish(new TweetCreatedNotification(entity.Id, entity.Message, entity.CreatorId,
                    entity.PostTime));
            }
            catch (ValidationException validationException)
            {
                _exceptionHandler?.HandleValidationException(validationException, @event.CreatorId);
            }
            catch (Exception miscException)
            {
                // _eventBus.Publish(new FailureNotification(miscException.Message, @event.CreatorId));
                throw;
            }
        }
    }
}