using System.Reflection;
using Kwetter.BuildingBlocks.Abstractions.Services;
using Kwetter.BuildingBlocks.Configurations.Extensions;
using Kwetter.BuildingBlocks.Configurations.Models;
using Kwetter.BuildingBlocks.EventBus.EventBus;
using Kwetter.BuildingBlocks.EventBus.EventBus.Interfaces;
using Kwetter.BuildingBlocks.EventBus.EventBusRabbitMQ;
using Kwetter.BuildingBlocks.IdentityBlocks;
using Kwetter.BuildingBlocks.KwetterLogger;
using Kwetter.Services.Tweet.Events.Notifications;
using Kwetter.Services.Tweet.Hub.EventHandlers;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace Kwetter.Services.Tweet.Hub
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var identityConfig = services.AddIdentityConfig(Configuration);
            var urlConfig = services.AddUrlConfig(Configuration);
            var eventBusConfig = services.AddEventBusConfig(Configuration);

            services.AddTransient<FailureNotificationHandler>();
            services.AddTransient<TweetCreatedNotificationHandler>();
            services.AddTransient<TweetLikedEventHandler>();

            services.AddHttpContextAccessor();
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.ConfigureKwetterLogger(Configuration);
            services.AddTransient<ICurrentUserService, CurrentUserService>();
            services.AddEventBus(eventBusConfig);
            services.AddKwetterIdentity(Environment, identityConfig);
            services.AddSignalR();

            services.AddHealthChecks();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                        .SetIsOriginAllowed(_ => true)
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IEventBus eventBus)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseKwetterLogger(env);
            
            eventBus.Subscribe<FailureNotification, FailureNotificationHandler>();
            eventBus.Subscribe<TweetCreatedNotification, TweetCreatedNotificationHandler>();
            eventBus.Subscribe<TweetLikedNotification, TweetLikedEventHandler>();

            app.UseRouting();
            app.UseCors("CorsPolicy");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapHub<TweetHub>("/hub/tweet"); });
        }
    }

    public static class CustomExtensionMethods
    {
        public static IServiceCollection AddEventBus(this IServiceCollection services,
            EventBusConfig eventBusConfig)
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

                return new EventBusRabbitMq(rabbitMqPersistentConnection, logger, eventBusSubscriptionsManager,
                    sp, subscriptionClientName, retryCount);
            });

            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();

            return services;
        }
    }
}