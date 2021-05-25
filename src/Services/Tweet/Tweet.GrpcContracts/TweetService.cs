using System;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Kwetter.Services.Tweet.GrpcContracts
{
    [ServiceContract(Name = "Tweet.TweetService")]
    public interface ITweetService
    {
        [OperationContract]
        ValueTask CreateTweet(CreateTweet tweet);
        
        [OperationContract]
        ValueTask<ContractTweetMessage> GetTweet(string id);
    }

    [DataContract]
    public class CreateTweet
    {
        [DataMember(Order = 1)]
        public string Message { get; set; }
        [DataMember(Order = 2)]
        public string UserId { get; set; }
        [DataMember(Order = 3)]
        public DateTime PostTime { get; set; }
    }
}