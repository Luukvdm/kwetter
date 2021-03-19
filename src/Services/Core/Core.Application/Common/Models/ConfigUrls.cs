namespace Kwetter.Services.Core.Application.Common.Models
{
    public class ConfigUrls
    {
        public const string AppSettingKey = "Urls";

        public string WebSpaClient { get; set; }
        public string ApiGateway { get; set; }
        public string IdentityServer { get; set; }
    }
}
