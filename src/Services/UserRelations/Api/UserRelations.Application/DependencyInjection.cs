using System.Reflection;
using Kwetter.BuildingBlocks.CQRS;
using Kwetter.BuildingBlocks.EventBus.EventBus.Interfaces;
using Kwetter.Services.UserRelations.Application.EventHandlers;
using Kwetter.Services.UserRelations.Events.Events;
using Microsoft.Extensions.DependencyInjection;

namespace Kwetter.Services.UserRelations.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddCqrsServices(Assembly.GetExecutingAssembly());

            services.AddTransient<CreateFollowEventHandler>();
            services.AddTransient<RemoveFollowEventHandler>();

            return services;
        }
    }

    public static class EventBusConfigurator
    {
        public static void ConfigureHandlers(IEventBus eventBus)
        {
            eventBus.Subscribe<CreateFollowEvent, CreateFollowEventHandler>();
            eventBus.Subscribe<RemoveFollowEvent, RemoveFollowEventHandler>();
        }
    }
}