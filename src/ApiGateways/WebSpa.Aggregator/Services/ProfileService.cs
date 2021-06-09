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
        private readonly TimeLineService _timelineService;
        private readonly UserService _userService;

        public ProfileService(GrpcChannelService grpcChannelService, ICurrentUserService currentUser,
            TimeLineService timelineService, UserService userService)
        {
            _grpcChannelService = grpcChannelService;
            _currentUser = currentUser;
            _timelineService = timelineService;
            _userService = userService;
        }

        public async Task<Profile> GetProfile(string username)
        {
            var user = await _userService.GetUserByUsername(username);

            return new Profile(user);
        }

        public async Task<Profile> GetMyProfile()
        {
            string userId = _currentUser.UserId;
            
            var user = await _userService.GetUser(userId);

            return new Profile(user);
        }
    }
}