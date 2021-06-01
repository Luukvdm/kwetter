using System.Threading.Tasks;
using Kwetter.ApiGateways.WebSpa.Aggregator.Models;
using Kwetter.BuildingBlocks.KwetterGrpc;
using Kwetter.Services.Identity.GrpcContracts;
using Kwetter.Services.Tweet.GrpcContracts;
using ProtoBuf.Grpc.Client;

namespace Kwetter.ApiGateways.WebSpa.Aggregator.Services
{
    public class TweetService
    {
        private readonly GrpcChannelService _grpcChannelService;

        public TweetService(GrpcChannelService grpcChannelService)
        {
            _grpcChannelService = grpcChannelService;
        }

        public async Task<Tweet> Get(int id)
        {
            var tweetChannel = await _grpcChannelService.CreateTweetChannel();
            var tweetService = tweetChannel.CreateGrpcService<ITweetService>();

            var identityChannel = await _grpcChannelService.CreateIdentityChannel();
            var accInfoService = identityChannel.CreateGrpcService<IAccountInformationService>();
            
            // TODO check for empty result
            var tweetResult = await tweetService.GetTweet(id.ToString());
            var accountResult = await accInfoService.GetBasicAccountInformation(tweetResult.CreatorId);

            return new Tweet(tweetResult)
            {
                Poster = new User(accountResult)
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