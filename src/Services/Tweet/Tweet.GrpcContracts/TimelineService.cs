using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Kwetter.Services.Tweet.GrpcContracts
{
    [ServiceContract]
    public interface ITimelineService
    {
        [OperationContract]
        ValueTask<IList<ContractTweetMessage>> GetTimeline(string userId);
    }

    [DataContract]
    public class ContractTweetMessage
    {
        [DataMember(Order = 1)]
        public int Id { get; set; }
        [DataMember(Order = 2)]
        public string Message { get; set; }
        [DataMember(Order = 3)]
        public string CreatorId { get; set; }
        [DataMember(Order = 4)]
        public DateTime PostTime { get; set; }
    }
}