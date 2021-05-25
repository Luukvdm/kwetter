using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
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
        private readonly ILogger<CreateTweetMessageEventHandler> _logger;

        public CreateTweetMessageEventHandler(ISender mediator, IEventBus eventBus,
            ILogger<CreateTweetMessageEventHandler> logger)
        {
            _mediator = mediator;
            _eventBus = eventBus;
            _logger = logger;
        }

        public async Task Handle(CreateTweetMessageEvent @event)
        {
            try
            {
                var entity =
                    await _mediator.Send(new CreateTweetMessageCommand(@event.Message, @event.CreatorId,
                        @event.PostTime));
                _eventBus.Publish(new TweetCreatedNotification(entity.Id, entity.Message, entity.CreatorId, entity.PostTime));
            }
            catch (ValidationException validationException)
            {
                _eventBus.Publish(new CreateTweetMessageFailedEvent(validationException.Message, @event.CreatorId));
                // await _tweetClientService.SendValidationException(notification.CreatorId, validationException);
            }
            // TODO replace with more precise exceptions
            catch (Exception miscException)
            {
                _logger.LogError(miscException, "Some create tweet message exception");
                // await _tweetClientService.SendException(notification.CreatorId, miscException);
            }
        }
    }
}