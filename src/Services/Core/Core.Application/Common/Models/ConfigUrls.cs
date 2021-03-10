namespace Kwetter.Services.Core.Application.Common.Models
{
    public class ConfigUrls
    {
        public const string AppSettingKey = "Urls";

        public string WebSpaClient { get; set; }
        public string ApiGateway { get; set; }
        public string IdentityServer { get; set; }
        public string EnvironmentService { get; set; }
        public string EnergyProductionService { get; set; }
        public string EnergyUsageService { get; set; }
    }
}
