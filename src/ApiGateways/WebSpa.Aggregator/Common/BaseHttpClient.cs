using System.Net.Http;
using Microsoft.Extensions.Logging;

namespace Kwetter.ApiGateways.WebSpa.Aggregator.Common
{
    public abstract class BaseHttpClient
    {
        protected readonly ILogger<BaseHttpClient> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        protected BaseHttpClient(ILogger<BaseHttpClient> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }
    }
}