using System.Linq;
using System.Threading.Tasks;
using Kwetter.ApiGateways.WebSpa.Aggregator.Models;
using Kwetter.BuildingBlocks.Abstractions.Services;
using Kwetter.BuildingBlocks.KwetterGrpc;
using Kwetter.Services.Identity.GrpcContracts;
using Kwetter.Services.Tweet.GrpcContracts;
using ProtoBuf.Grpc.Client;

namespace Kwetter.ApiGateways.WebSpa.Aggregator.Services
{
    public class TimeLineService
    {
        private readonly GrpcChannelService _grpcChannelService;

        private readonly IDateTime _dateTime;

        public TimeLineService(GrpcChannelService grpcChannelService, IDateTime dateTime)
        {
            _grpcChannelService = grpcChannelService;
            _dateTime = dateTime;
        }

        public async Task<TimeLine> GetTimeLine(string userId)
        {
            var tweetChannel = await _grpcChannelService.CreateTweetChannel();
            var timelineService = tweetChannel.CreateGrpcService<ITimelineService>();
            var tweets = await timelineService.GetTimeline(userId);
            string[] userIds = tweets.Select(e => e.CreatorId).Distinct().ToArray();

            var identityChannel = await _grpcChannelService.CreateIdentityChannel();
            var accInfoService = identityChannel.CreateGrpcService<IAccountInformationService>();
            var userInfos = await accInfoService.GetBasicAccountsInformation(userIds);
            var users = userInfos.Select(e => new User(e));

            var timeline = new TimeLine(_dateTime.Now);

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