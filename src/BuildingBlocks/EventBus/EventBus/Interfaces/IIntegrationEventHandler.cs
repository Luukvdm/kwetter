using System.Threading.Tasks;
using Kwetter.BuildingBlocks.EventBus.EventBus.Events;

namespace Kwetter.BuildingBlocks.EventBus.EventBus.Interfaces
{
    public interface IIntegrationEventHandler<in TIntegrationEvent> : IIntegrationEventHandler
        where TIntegrationEvent : IntegrationEvent
    {
        Task Handle(TIntegrationEvent @event);
    }
    
    public interface IIntegrationEventHandler
    {
    }
    /* public interface IIntegrationEventHandler<in TIntegrationEvent> : INotificationHandler<TIntegrationEvent>
        where TIntegrationEvent : IntegrationEvent
    {
        Task Handle(TIntegrationEvent @event, CancellationToken cancellationToken);
    } */
}