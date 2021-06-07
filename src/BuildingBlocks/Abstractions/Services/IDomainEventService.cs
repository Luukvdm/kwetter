using System.Threading.Tasks;
using Kwetter.BuildingBlocks.KwetterDomain;

namespace Kwetter.BuildingBlocks.Abstractions.Services
{
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}