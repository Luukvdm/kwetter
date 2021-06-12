using System.Threading.Tasks;
using Kwetter.BuildingBlocks.Abstractions.Services;
using Kwetter.BuildingBlocks.CQRS.Exceptions;
using Kwetter.BuildingBlocks.CQRS.Services;
using Kwetter.BuildingBlocks.EventBus.EventBus.Interfaces;
using Kwetter.Services.Core.Tweet.Application.Commands.CreateLike;
using Kwetter.Services.Tweet.Events.Events;
using Kwetter.Services.Tweet.Events.Notifications;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Kwetter.Services.Core.Tweet.Application.EventHandlers
{
    public class CreateLikeEventHandler : IIntegrationEventHandler<CreateLikeEvent>
    {
        private readonly ISender _mediator;
        private readonly IEventBus _eventBus;
        private readonly IExceptionHandler _exceptionHandler;
        private readonly ILogger<CreateLikeEventHandler> _logger;

        public CreateLikeEventHandler(ISender mediator, IEventBus eventBus, IExceptionHandler exceptionHandler, ILogger<CreateLikeEventHandler> logger)
        {
            _mediator = mediator;
            _eventBus = eventBus;
            _exceptionHandler = exceptionHandler;
            _logger = logger;
        }

        public async Task Handle(CreateLikeEvent @event)
        {
            try
            {
                var entity = await _mediator.Send(new CreateLikeCommand(@event.LikerId, @event.LikeTime, @event.TweetId));
                _eventBus.Publish(new TweetLikedNotification(entity.UserId, entity.PostTime, entity.TweetMessageId));
            }
            catch (ValidationException validationException)
            {
                _exceptionHandler?.HandleValidationException(validationException, @event.LikerId);
                throw;
            }
        }
    }
}