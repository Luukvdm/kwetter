using System.Reflection;
using Kwetter.BuildingBlocks.CQRS;
using Kwetter.BuildingBlocks.EventBus.EventBus.Interfaces;
using Kwetter.Services.Tweet.Application.EventHandlers;
using Kwetter.Services.Tweet.Events.Events;
using Microsoft.Extensions.DependencyInjection;

namespace Kwetter.Services.Tweet.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddCqrsServices(Assembly.GetExecutingAssembly());

            services.AddTransient<CreateTweetMessageEventHandler>();
            services.AddTransient<CreateLikeEventHandler>();

            return services;
        }
    }

    public static class EventBusConfigurator
    {
        public static void ConfigureHandlers(IEventBus eventBus)
        {
            eventBus.Subscribe<CreateTweetMessageEvent, CreateTweetMessageEventHandler>();
            eventBus.Subscribe<CreateLikeEvent, CreateLikeEventHandler>();
        }
    }
}
