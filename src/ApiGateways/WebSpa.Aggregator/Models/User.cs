using Kwetter.Services.Identity.GrpcContracts;

namespace Kwetter.ApiGateways.WebSpa.Aggregator.Models
{
    public class User
    {
        public User(string id)
        {
            Id = id;
        }

        public User(PublicAccount accInfo, bool isCurrentUser, bool isFollowing = false)
        {
            Id = accInfo.Id;
            DisplayName = accInfo.DisplayName;
            Username = accInfo.Username;
            ProfilePicture = accInfo.ProfilePicture;

            IsCurrentUser = isCurrentUser;
            IsFollowing = isFollowing;
        }
        
        public string Id { get; set; }
        public string DisplayName { get; set; }
        public string Username { get; set; }
        public string ProfilePicture { get; set; }
        public bool IsCurrentUser { get; set; }
        public bool IsFollowing { get; set; }
    }
}