using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Kwetter.Services.UserRelations.GrpcContracts
{
    [ServiceContract(Name = "UserRelations.FollowingService")]
    public interface IFollowingService
    {
        [OperationContract]
        ValueTask<IList<string>> GetFollowed(string userId);

        [OperationContract]
        ValueTask<IList<string>> GetFollowers(string userId);

        [OperationContract]
        ValueTask CreateFollowing(CreateFollowing following);

        [OperationContract]
        ValueTask RemoveFollowing(RemoveFollowing following);
    }

    [DataContract]
    public class RemoveFollowing
    {
        public RemoveFollowing()
        {
        }

        public RemoveFollowing(string followingUserId, string followedUserId)
        {
            FollowingUserId = followingUserId;
            FollowedUserId = followedUserId;
        }

        [DataMember(Order = 1)] public string FollowedUserId { get; set; }
        [DataMember(Order = 2)] public string FollowingUserId { get; set; }
    }

    [DataContract]
    public class CreateFollowing
    {
        public CreateFollowing()
        {
        }

        public CreateFollowing(string followingUserId, string followedUserId)
        {
            FollowingUserId = followingUserId;
            FollowedUserId = followedUserId;
        }

        [DataMember(Order = 1)] public string FollowedUserId { get; set; }
        [DataMember(Order = 2)] public string FollowingUserId { get; set; }
    }
}