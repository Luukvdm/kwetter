using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kwetter.ApiGateways.WebSpa.Aggregator.Models;
using Kwetter.BuildingBlocks.Abstractions.Services;
using Kwetter.BuildingBlocks.KwetterGrpc;
using Kwetter.Services.UserRelations.GrpcContracts;
using ProtoBuf.Grpc.Client;

namespace Kwetter.ApiGateways.WebSpa.Aggregator.Services
{
    public class FollowingService
    {
        private readonly GrpcChannelService _grpcChannelService;
        private readonly UserService _userService;

        public FollowingService(GrpcChannelService grpcChannelService, UserService userService)
        {
            _grpcChannelService = grpcChannelService;
            _userService = userService;
        }

        public async Task CreateFollowing(string followingUserId, string followedUserId)
        {
            var userRelationsChannel = await _grpcChannelService.CreateUserRelationsChannel();
            var followingService = userRelationsChannel.CreateGrpcService<IFollowingService>();

            await followingService.CreateFollowing(new CreateFollowing(followingUserId, followedUserId));
        }
        
        public async Task RemoveFollowing(string followingUserId, string followedUserId)
        {
            var userRelationsChannel = await _grpcChannelService.CreateUserRelationsChannel();
            var followingService = userRelationsChannel.CreateGrpcService<IFollowingService>();

            await followingService.RemoveFollowing(new RemoveFollowing(followingUserId, followedUserId));
        }
        
        public async Task<IList<string>> GetFollowed(string userId)
        {
            var userRelationsChannel = await _grpcChannelService.CreateUserRelationsChannel();
            var followingService = userRelationsChannel.CreateGrpcService<IFollowingService>();

            return await followingService.GetFollowed(userId);
        }

        public async Task<IList<User>> GetFollowedUsers(string userId)
        {
            var userRelationsChannel = await _grpcChannelService.CreateUserRelationsChannel();
            var followingService = userRelationsChannel.CreateGrpcService<IFollowingService>();

            var userIds = await followingService.GetFollowed(userId);
            var users = await _userService.GetUsers(userIds.ToArray());

            return users;
        }

        public async Task<IList<string>> GetFollowers(string userId)
        {
            var userRelationsChannel = await _grpcChannelService.CreateUserRelationsChannel();
            var followingService = userRelationsChannel.CreateGrpcService<IFollowingService>();

            return await followingService.GetFollowers(userId);
        }

        public async Task<IList<User>> GetFollowersUsers(string userId)
        {
            var userRelationsChannel = await _grpcChannelService.CreateUserRelationsChannel();
            var followingService = userRelationsChannel.CreateGrpcService<IFollowingService>();

            var userIds = await followingService.GetFollowers(userId);
            var users = await _userService.GetUsers(userIds.ToArray());

            return users;
        }
    }
}