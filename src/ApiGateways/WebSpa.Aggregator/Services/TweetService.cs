using System.Threading.Tasks;
using Kwetter.ApiGateways.WebSpa.Aggregator.Models;
using Kwetter.BuildingBlocks.KwetterGrpc;
using Kwetter.Services.Tweet.GrpcContracts;
using ProtoBuf.Grpc.Client;

namespace Kwetter.ApiGateways.WebSpa.Aggregator.Services
{
    public class TweetService
    {
        private readonly GrpcChannelService _grpcChannelService;

        private readonly UserService _userService;

        public TweetService(GrpcChannelService grpcChannelService, UserService userService)
        {
            _grpcChannelService = grpcChannelService;
            _userService = userService;
        }

        public async Task<Tweet> Get(int id)
        {
            var tweetChannel = await _grpcChannelService.CreateTweetChannel();
            var tweetService = tweetChannel.CreateGrpcService<ITweetService>();
            
            // TODO check for empty result
            var tweetResult = await tweetService.GetTweet(id.ToString());
            var user = await _userService.GetUser(tweetResult.CreatorId);

            return new Tweet(tweetResult)
            {
                Poster = user
            };
        }

        public async Task Create(CreateTweet tweetObject)
        {
            var tweetChannel = await _grpcChannelService.CreateTweetChannel();
            var tweetService = tweetChannel.CreateGrpcService<ITweetService>();
            
            await tweetService.CreateTweet(tweetObject);
        }

        public async Task Like(CreateLike likeObject)
        {
            var tweetChannel = await _grpcChannelService.CreateTweetChannel();
            var tweetService = tweetChannel.CreateGrpcService<ITweetService>();

            await tweetService.LikeTweet(likeObject);
        }
    }
}