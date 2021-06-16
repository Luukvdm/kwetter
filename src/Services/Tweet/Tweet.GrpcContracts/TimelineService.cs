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
        ValueTask<IList<ContractTweetMessage>> GetTimeline(string userId);
    }
}