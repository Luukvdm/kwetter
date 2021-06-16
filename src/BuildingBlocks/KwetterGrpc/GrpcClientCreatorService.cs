using System.Net.Http;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Net.Client;
using IdentityModel.AspNetCore.AccessTokenManagement;
using Kwetter.BuildingBlocks.Configurations.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using ProtoBuf.Grpc.Client;

namespace Kwetter.BuildingBlocks.KwetterGrpc
{
    public class GrpcClientCreatorService
    {
        private readonly UrlConfig _urlConfig;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IClientAccessTokenManagementService _tokenManagementService;

        private readonly string _accessManagementClientName;
        private readonly string _httpClientName;

        public GrpcClientCreatorService(UrlConfig urlConfig,
            IHttpClientFactory httpClientFactory, IClientAccessTokenManagementService tokenManagementService,
            string accessManagementClientName = "default-token-client", string httpClientName = "default-client")
        {
            _urlConfig = urlConfig;
            _httpClientFactory = httpClientFactory;
            _tokenManagementService = tokenManagementService;

            _accessManagementClientName = accessManagementClientName;
            _httpClientName = httpClientName;
        }

        private async Task<CallCredentials> CreateCallCredentials()
        {
            string token = await _tokenManagementService.GetClientAccessTokenAsync(_accessManagementClientName);

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

            var httpClient = _httpClientFactory.CreateClient(_httpClientName);

            string token = await _tokenManagementService.GetClientAccessTokenAsync(_accessManagementClientName);
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

        public async Task<GrpcChannel> CreateUserRelationsChannel()
        {
            string url = _urlConfig.UserRelationsGrpc;
            return await CreateChannel(url);
        }
    }
}