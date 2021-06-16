using System.Collections.Generic;

namespace Kwetter.ApiGateways.WebSpa.Aggregator.Models
{
    public class Profile
    {
        public Profile(User user)
        {
            Id = user.Id;
            DisplayName = user.DisplayName;
            Username = user.Username;
            ProfilePicture = user.ProfilePicture;
            IsCurrentUser = user.IsCurrentUser;
            IsFollowing = user.IsFollowing;
        }

        public string Id { get; set; }
        public string DisplayName { get; set; }
        public string Username { get; set; }
        public string ProfilePicture { get; set; }

        public string BannerImage { get; set; } =
            "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Foptimizedude.com%2Fwp-content%2Fuploads%2F2015%2F02%2FCool-Twitter-Headers-5.jpg";
        public bool IsCurrentUser { get; set; }
        public IList<User> Following { get; set; }
        public IList<User> Followers { get; set; }
        public bool IsFollowing { get; set; }
    }
}