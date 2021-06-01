using System.ComponentModel.Design;
using Kwetter.BuildingBlocks.Abstractions.Services;
using Kwetter.BuildingBlocks.Configurations.Models;
using Kwetter.BuildingBlocks.EventBus.EventBus;
using Kwetter.BuildingBlocks.EventBus.EventBus.Interfaces;
using Kwetter.BuildingBlocks.EventBus.EventBusRabbitMQ;
using Kwetter.Services.Core.Tweet.Application.Common.Interfaces;
using Kwetter.Services.Tweet.Infrastructure.Persistence;
using Kwetter.Services.Tweet.Infrastructure.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace Kwetter.Services.Tweet.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase("EnvironmentDb"));
            }
            else
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseNpgsql(
                        configuration.GetConnectionString("DefaultConnection"),
                        b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            }

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());
            services.AddTransient<IDateTime, DateTimeService>();

            return services;
        }

        public static IServiceCollection AddEventBus(this IServiceCollection services, EventBusConfig eventBusConfig)
        {

            services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();

                var factory = new ConnectionFactory
                {
                    HostName = eventBusConfig.HostName,
                    DispatchConsumersAsync = true
                };

                if (!string.IsNullOrEmpty(eventBusConfig.Username)) factory.UserName = eventBusConfig.Username;
                if (!string.IsNullOrEmpty(eventBusConfig.Password)) factory.Password = eventBusConfig.Password;
                int retryCount = eventBusConfig.RetryCount == 0 ? 5 : eventBusConfig.RetryCount;

                return new DefaultRabbitMQPersistentConnection(factory, logger, retryCount);
            });

            services.AddSingleton<IEventBus, EventBusRabbitMq>(sp =>
            {
                string subscriptionClientName = eventBusConfig.ClientName;

                var rabbitMqPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
                var logger = sp.GetRequiredService<ILogger<EventBusRabbitMq>>();
                var eventBusSubscriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

                int retryCount = eventBusConfig.RetryCount == 0 ? 5 : eventBusConfig.RetryCount;
                
                
                // var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
                return new EventBusRabbitMq(rabbitMqPersistentConnection, logger, eventBusSubscriptionsManager, sp, subscriptionClientName, retryCount);
            });

            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();

            return services;
        }
    }
}