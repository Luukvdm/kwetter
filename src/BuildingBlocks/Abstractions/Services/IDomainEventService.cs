using System.Threading.Tasks;
using Kwetter.BuildingBlocks.Core.Domain.Common;

namespace Kwetter.BuildingBlocks.Abstractions.Services
{
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}