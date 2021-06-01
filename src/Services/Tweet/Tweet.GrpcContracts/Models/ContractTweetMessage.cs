using System;
using System.Runtime.Serialization;

namespace Kwetter.Services.Tweet.GrpcContracts.Models
{
    [DataContract]
    public class ContractTweetMessage
    {
        [DataMember(Order = 1)]
        public int Id { get; set; }
        [DataMember(Order = 2)]
        public string Message { get; set; }
        [DataMember(Order = 3)] 
        public int Likes { get; set; }
        [DataMember(Order = 4)] 
        public string CreatorId { get; set; }
        [DataMember(Order = 5)]
        public DateTime PostTime { get; set; }
    }
}