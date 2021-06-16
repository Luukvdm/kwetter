using System;
using System.Net.Http;
using IdentityModel.AspNetCore.AccessTokenManagement;
using Kwetter.BuildingBlocks.Configurations.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Kwetter.BuildingBlocks.KwetterGrpc
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddGrpcClientServices(this IServiceCollection services,
            string accessManagementClientName, string httpClientName, bool requireSsl = false)
        {
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", !requireSsl);

            services.AddTransient<GrpcClientCreatorService>(sp =>
            {
                var urlConfig = sp.GetRequiredService<UrlConfig>();
                var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
                var tokenManagementService = sp.GetRequiredService<IClientAccessTokenManagementService>();
                
                return new GrpcClientCreatorService(urlConfig, httpClientFactory, tokenManagementService,
                    accessManagementClientName, httpClientName);
            });
            services.AddSingleton<GrpcChannelService>();

            return services;
        }
    }
}