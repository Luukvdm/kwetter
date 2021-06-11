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
        ValueTask<IList<string>> GetFollowedUserIds(string userId);
        [OperationContract]
        ValueTask CreateFollowing(CreateFollowing following);
    }
    
    [DataContract]
    public class CreateFollowing
    {
        [DataMember(Order = 1)] public string FollowedUserId { get; set; }
        [DataMember(Order = 2)] public string FollowingUserId { get; set; }
    }
}