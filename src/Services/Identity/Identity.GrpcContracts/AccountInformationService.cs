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
        ValueTask<PublicAccount> GetAccount(string userId);
        
        [OperationContract]
        ValueTask<PublicAccount> GetAccountByUsername(string username);
        
        [OperationContract]
        ValueTask<IList<PublicAccount>> GetAccounts(string[] userIds);
    }

    [DataContract]
    public class PublicAccount
    {
        [DataMember(Order = 1)]
        public string Id { get; set; }

        [DataMember(Order = 2)]
        public string DisplayName { get; set; }

        [DataMember(Order = 3)]
        public string Username { get; set; }

        [DataMember(Order = 4)]
        public string ProfilePicture { get; set; }
    }
}