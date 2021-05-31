using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Kwetter.Services.Identity.GrpcContracts
{
    [ServiceContract(Name = "Identity.AccountInformationService")]
    public interface IAccountInformationService
    {
        [OperationContract]
        ValueTask<BasicAccountInformation> GetBasicAccountInformation(string userId);
        
        [OperationContract]
        ValueTask<IList<BasicAccountInformation>> GetBasicAccountsInformation(string[] userIds);
    }

    [DataContract]
    public class BasicAccountInformation
    {
        [DataMember(Order = 1)]
        public string Id { get; set; }

        [DataMember(Order = 2)]
        public string DisplayName { get; set; }

        [DataMember(Order = 3)]
        public string UserName { get; set; }

        [DataMember(Order = 4)]
        public string ProfilePicture { get; set; }
    }
}