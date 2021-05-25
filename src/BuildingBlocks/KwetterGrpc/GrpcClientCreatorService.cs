using System.Net.Http;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Net.Client;
using Kwetter.BuildingBlocks.Configurations.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using ProtoBuf.Grpc.Client;

namespace Kwetter.BuildingBlocks.KwetterGrpc
{
    public class GrpcClientCreatorService
    {
        private readonly UrlConfig _urlConfig;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IHttpClientFactory _httpClientFactory;

        public GrpcClientCreatorService(UrlConfig urlConfig, IHttpContextAccessor httpContext,
            IHttpClientFactory httpClientFactory)
        {
            _urlConfig = urlConfig;
            _httpContext = httpContext;
            _httpClientFactory = httpClientFactory;
        }

        private async Task<CallCredentials> CreateCallCredentials()
        {
            string token = await _httpContext.HttpContext.GetClientAccessTokenAsync();

            var credentials = CallCredentials.FromInterceptor((_, metadata) =>
            {
                if (!string.IsNullOrEmpty(token))
                {
                    metadata.Add("Authorization", $"Bearer {token}");
                }

                return Task.CompletedTask;
            });

            return credentials;
        }

        private async Task<ChannelCredentials> CreateChannelCredentials(bool useSsl,
            CallCredentials? callCredentials = null)
        {
            var callCreds = callCredentials ?? await CreateCallCredentials();
            // var callCreds = await CreateCallCredentials();

            var creds = useSsl
                ? ChannelCredentials.Create(new SslCredentials(), callCreds)
                : ChannelCredentials.Insecure;
            return creds;
        }

        public async Task<GrpcChannel> CreateChannel(string url)
        {
            bool useSsl = url.StartsWith("https");

            if (!useSsl) GrpcClientFactory.AllowUnencryptedHttp2 = true;
            
            var httpClient = _httpClientFactory.CreateClient("default-client");
            
            string token = await _httpContext.HttpContext.GetClientAccessTokenAsync("default-client");
            // string token = httpClient.DefaultRequestHeaders.Authorization?.Parameter;
            var callCreds = CallCredentials.FromInterceptor((_, metadata) =>
            {
                metadata.Add("Authorization", $"Bearer {token}");

                return Task.CompletedTask;
            });

            var channelCreds = await CreateChannelCredentials(useSsl, callCreds);

            var http = GrpcChannel.ForAddress(url, new GrpcChannelOptions
            {
                // Try http client with access token
                HttpClient = httpClient,
                Credentials = channelCreds
            });

            return http;
        }

        public async Task<GrpcChannel> CreateIdentityServerChannel()
        {
            string url = _urlConfig.IdentityServerGrpc;
            return await CreateChannel(url);
        }

        public async Task<GrpcChannel> CreateTweetChannel()
        {
            string url = _urlConfig.TweetGrpc;
            return await CreateChannel(url);
        }
    }
}