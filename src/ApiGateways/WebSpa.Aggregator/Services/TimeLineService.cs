using System.Linq;
using System.Threading.Tasks;
using Kwetter.ApiGateways.WebSpa.Aggregator.Models;
using Kwetter.BuildingBlocks.Abstractions.Exceptions;
using Kwetter.BuildingBlocks.Abstractions.Services;
using Kwetter.BuildingBlocks.KwetterGrpc;
using Kwetter.Services.Tweet.GrpcContracts;
using ProtoBuf.Grpc.Client;

namespace Kwetter.ApiGateways.WebSpa.Aggregator.Services
{
    public class TimeLineService
    {
        private readonly GrpcChannelService _grpcChannelService;

        private readonly UserService _userService;
        private readonly FollowingService _followingService;
        private readonly IDateTime _dateTime;

        public TimeLineService(GrpcChannelService grpcChannelService, IDateTime dateTime, UserService userService, FollowingService followingService)
        {
            _grpcChannelService = grpcChannelService;
            _dateTime = dateTime;
            _userService = userService;
            _followingService = followingService;
        }

        public async Task<Timeline> GetTimeLine(string username)
        {
            var owner = await _userService.GetUserByUsername(username);
            if (owner == null) throw new NotFoundException("User {username} doesn't exist");
            
            var tweetChannel = await _grpcChannelService.CreateTweetChannel();
            var timelineService = tweetChannel.CreateGrpcService<ITimelineService>();

            var followedUsers = await _followingService.GetFollowed(owner.Id);
            var tweets = await timelineService.GetTimeline(new GetTimeline(owner.Id, followedUsers.ToArray()));
            string[] userIds = tweets.Select(e => e.CreatorId).Distinct().ToArray();

            var users = await _userService.GetUsers(userIds);

            var timeline = new Timeline(_dateTime.Now, owner.Username, owner.Id);

            foreach (var tweetDto in tweets)
            {
                var tweet = new Tweet(tweetDto)
                {
                    Poster = users.SingleOrDefault(e => e.Id == tweetDto.CreatorId)
                };
                timeline.Tweets.Add(tweet);
            }

            return timeline;
        }
    }
}