using Kwetter.Services.Identity.GrpcContracts;

namespace Kwetter.ApiGateways.WebSpa.Aggregator.Models
{
    public class User
    {
        public User(string id)
        {
            Id = id;
        }

        public User(BasicAccountInformation accInfo)
        {
            Id = accInfo.Id;
            FirstName = accInfo.FirstName;
            LastName = accInfo.LastName;
            UserName = accInfo.UserName;
            ProfilePicture = accInfo.ProfilePicture;
        }
        
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string ProfilePicture { get; set; }
    }
}