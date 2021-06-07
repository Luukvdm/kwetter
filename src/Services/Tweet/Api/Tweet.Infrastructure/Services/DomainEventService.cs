using System;
using System.Threading.Tasks;
using Kwetter.BuildingBlocks.Abstractions.Services;
using Kwetter.BuildingBlocks.CQRS.Models;
using Kwetter.BuildingBlocks.KwetterDomain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Kwetter.Services.Tweet.Infrastructure.Services
{
    public class DomainEventService : IDomainEventService
    {
        private readonly ILogger<DomainEventService> _logger;
        private readonly IMediator _mediator;

        public DomainEventService(ILogger<DomainEventService> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public async Task Publish(DomainEvent domainEvent)
        {
            await _mediator.Publish(GetNotificationCorrespondingToDomainEvent(domainEvent));
        }

        private INotification GetNotificationCorrespondingToDomainEvent(DomainEvent domainEvent)
        {
            return (INotification) Activator.CreateInstance(
                typeof(DomainEventNotification<>).MakeGenericType(domainEvent.GetType()), domainEvent);
        }
    }
}