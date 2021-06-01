using System;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Threading.Tasks;
using Kwetter.Services.Tweet.GrpcContracts.Models;

namespace Kwetter.Services.Tweet.GrpcContracts
{
    [ServiceContract(Name = "Tweet.TweetService")]
    public interface ITweetService
    {
        [OperationContract]
        ValueTask CreateTweet(CreateTweet tweet);

        [OperationContract]
        ValueTask<ContractTweetMessage> GetTweet(string id);

        [OperationContract]
        ValueTask LikeTweet(CreateLike like);
    }

    [DataContract]
    public class CreateTweet
    {
        [DataMember(Order = 1)] public string Message { get; set; }
        [DataMember(Order = 2)] public string UserId { get; set; }
        [DataMember(Order = 3)] public DateTime PostTime { get; set; }
    }

    [DataContract]
    public class CreateLike
    {
        [DataMember(Order = 1)] public int TweetId { get; set; }
        [DataMember(Order = 2)] public string UserId { get; set; }
        [DataMember(Order = 3)] public DateTime LikeTime { get; set; }
    }
}