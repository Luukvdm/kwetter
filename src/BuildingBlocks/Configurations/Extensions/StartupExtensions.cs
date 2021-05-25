using Kwetter.BuildingBlocks.Configurations.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kwetter.BuildingBlocks.Configurations.Extensions
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddCoreConfigurations(this IServiceCollection services,
            IConfiguration configuration)
        {
            var configUrls = new UrlConfig();
            configuration.GetSection(UrlConfig.AppSettingKey).Bind(configUrls);
            services.AddSingleton(s => configUrls);

            var identityConfig = new IdentityConfig();
            configuration.GetSection(IdentityConfig.AppSettingKey).Bind(identityConfig);
            services.AddSingleton(s => identityConfig);

            return services;
        }

        public static UrlConfig AddUrlConfig(this IServiceCollection services, IConfiguration configuration)
        {
            var configUrls = new UrlConfig();
            configuration.GetSection(UrlConfig.AppSettingKey).Bind(configUrls);
            services.AddSingleton(s => configUrls);
            return configUrls;
        }
        
        public static IdentityConfig AddIdentityConfig(this IServiceCollection services, IConfiguration configuration)
        {
            var identityConfig = new IdentityConfig();
            configuration.GetSection(IdentityConfig.AppSettingKey).Bind(identityConfig);
            services.AddSingleton(s => identityConfig);
            return identityConfig;
        }

        public static EventBusConfig AddEventBusConfig(this IServiceCollection services, IConfiguration configuration)
        {
            var eventBusConfig = new EventBusConfig();
            configuration.GetSection(EventBusConfig.AppSettingKey).Bind(eventBusConfig);
            services.AddSingleton(s => eventBusConfig);
            return eventBusConfig;
        }
    }
}