namespace Kwetter.BuildingBlocks.Configurations.Models
{
    public class EventBusConfig
    {
        public const string AppSettingKey = "EventBus";

        public string Username { get; set; }
        public string Password { get; set; }
        public string ClientName { get; set; }
        public int RetryCount { get; set; }
        public string HostName { get; set; }
    }
}