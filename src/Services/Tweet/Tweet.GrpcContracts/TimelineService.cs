using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Threading.Tasks;
using Kwetter.Services.Tweet.GrpcContracts.Models;

namespace Kwetter.Services.Tweet.GrpcContracts
{
    [ServiceContract]
    public interface ITimelineService
    {
        [OperationContract]
        ValueTask<IList<ContractTweetMessage>> GetTimeline(GetTimeline getTimeline);
    }

    [DataContract]
    public class GetTimeline
    {
        public GetTimeline(string userId, string[] followedUserIds)
        {
            UserId = userId;
            FollowedUserIds = followedUserIds;
        }

        public GetTimeline()
        {
        }

        [DataMember(Order = 1)] public string UserId { get; set; }
        [DataMember(Order = 2)] public string[] FollowedUserIds { get; set; }
    }
}