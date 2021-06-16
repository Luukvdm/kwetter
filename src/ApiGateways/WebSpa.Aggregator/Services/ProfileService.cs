using System.Linq;
using System.Threading.Tasks;
using Kwetter.ApiGateways.WebSpa.Aggregator.Models;
using Kwetter.BuildingBlocks.Abstractions.Services;
using Kwetter.BuildingBlocks.KwetterGrpc;

namespace Kwetter.ApiGateways.WebSpa.Aggregator.Services
{
    public class ProfileService
    {
        private readonly GrpcChannelService _grpcChannelService;
        private readonly ICurrentUserService _currentUser;
        private readonly UserService _userService;
        private readonly FollowingService _followingService;

        public ProfileService(GrpcChannelService grpcChannelService, ICurrentUserService currentUser,
            UserService userService, FollowingService followingService)
        {
            _grpcChannelService = grpcChannelService;
            _currentUser = currentUser;
            _userService = userService;
            _followingService = followingService;
        }

        public async Task<Profile> GetProfile(string username)
        {
            var user = await _userService.GetUserByUsername(username);

            var profile = new Profile(user)
            {
                Followers = await _followingService.GetFollowersUsers(user.Id),
                Following = await _followingService.GetFollowedUsers(user.Id)
            };
            profile.IsFollowing = profile.Followers.Any(e => e.Id == _currentUser.UserId);

            return profile;
        }

        public async Task<Profile> GetMyProfile()
        {
            string userId = _currentUser.UserId;

            var user = await _userService.GetUser(userId);
            var profile = new Profile(user)
            {
                Followers = await _followingService.GetFollowersUsers(user.Id),
                Following = await _followingService.GetFollowedUsers(user.Id)
            };

            return profile;
        }
    }
}