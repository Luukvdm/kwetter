namespace Kwetter.BuildingBlocks.Configurations.Models
{
    public class UrlConfig
    {
        public const string AppSettingKey = "Urls";

        public string WebSpaClient { get; set; }
        public string ApiGateway { get; set; }
        
        public string TweetApi { get; set; }
        public string TweetGrpc { get; set; }
        public string UserRelationsApi { get; set; }
        public string UserRelationsGrpc { get; set; }
        public string IdentityServerApi { get; set; }
        public string IdentityServerGrpc { get; set; }
    }
}