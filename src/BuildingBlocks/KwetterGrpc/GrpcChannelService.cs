using System.Threading.Tasks;
using Grpc.Net.Client;

namespace Kwetter.BuildingBlocks.KwetterGrpc
{
    public class GrpcChannelService
    {
        private readonly GrpcClientCreatorService _grpcClientCreator;

        private GrpcChannel _tweetChannel;
        private GrpcChannel _identityChannel;
        private GrpcChannel _userRelationsChannel;

        public GrpcChannelService(GrpcClientCreatorService grpcClientCreator)
        {
            _grpcClientCreator = grpcClientCreator;
        }

        public async Task<GrpcChannel> CreateTweetChannel()
        {
            _tweetChannel ??= await _grpcClientCreator.CreateTweetChannel();
            return _tweetChannel;
        }

        public async Task<GrpcChannel> CreateIdentityChannel()
        {
            _identityChannel ??= await _grpcClientCreator.CreateIdentityServerChannel();
            return _identityChannel;
        }

        public async Task<GrpcChannel> CreateUserRelationsChannel()
        {
            _userRelationsChannel ??= await _grpcClientCreator.CreateUserRelationsChannel();
            return _userRelationsChannel;
        }
    }
}