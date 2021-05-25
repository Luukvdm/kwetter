using System;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Kwetter.BuildingBlocks.KwetterGrpc
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddGrpcClientServices(this IServiceCollection services, string token, bool useSsl = false, Action<HttpClient> configureClient = null)
        {
            services.AddTransient<GrpcClientCreatorService>();
            services.AddSingleton<GrpcChannelService>();

            return services;
        }
    }
}