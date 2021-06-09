using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kwetter.ApiGateways.WebSpa.Aggregator.Models;
using Kwetter.BuildingBlocks.Abstractions.Services;
using Kwetter.BuildingBlocks.KwetterGrpc;
using Kwetter.Services.Identity.GrpcContracts;
using ProtoBuf.Grpc.Client;

namespace Kwetter.ApiGateways.WebSpa.Aggregator.Services
{
    public class UserService
    {
        private readonly GrpcChannelService _grpcChannelService;
        private readonly ICurrentUserService _currentUserService;

        public UserService(GrpcChannelService grpcChannelService, ICurrentUserService currentUserService)
        {
            _grpcChannelService = grpcChannelService;
            _currentUserService = currentUserService;
        }

        public async Task<IList<User>> GetUsers(string[] userIds)
        {
            var identityChannel = await _grpcChannelService.CreateIdentityChannel();
            var accInfoService = identityChannel.CreateGrpcService<IAccountInformationService>();

            var userInfos = await accInfoService.GetAccounts(userIds);
            var users = userInfos.Select(e =>
            {
                bool isCurrent = e.Id == _currentUserService.UserId;
                return new User(e, isCurrent);
            });

            return users.ToList();
        }

        public async Task<User> GetUser(string userId)
        {
            var identityChannel = await _grpcChannelService.CreateIdentityChannel();
            var accInfoService = identityChannel.CreateGrpcService<IAccountInformationService>();

            var userInfo = await accInfoService.GetAccount(userId);

            bool isCurrent = userInfo.Id == _currentUserService.UserId;
            return new User(userInfo, isCurrent);
        }

        public async Task<User> GetUserByUsername(string username)
        {
            var identityChannel = await _grpcChannelService.CreateIdentityChannel();
            var accInfoService = identityChannel.CreateGrpcService<IAccountInformationService>();

            var userInfo = await accInfoService.GetAccountByUsername(username);

            bool isCurrent = userInfo.Id == _currentUserService.UserId;
            return new User(userInfo, isCurrent);
        }
    }
}