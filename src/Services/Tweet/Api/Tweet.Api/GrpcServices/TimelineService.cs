using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kwetter.BuildingBlocks.KwetterGrpc;
using Kwetter.Services.Tweet.Application.Queries.GetTimeline;
using Kwetter.Services.Tweet.GrpcContracts;
using Kwetter.Services.Tweet.GrpcContracts.Models;
using Kwetter.Services.UserRelations.GrpcContracts;
using MediatR;
using ProtoBuf.Grpc.Client;

namespace Kwetter.Services.Tweet.Api.GrpcServices
{
    public class TimelineService : ITimelineService
    {
        private readonly ISender _mediator;
        private readonly GrpcChannelService _grpcChannelService;

        public TimelineService(ISender mediator, GrpcChannelService grpcChannelService)
        {
            _mediator = mediator;
            _grpcChannelService = grpcChannelService;
        }

        public async ValueTask<IList<ContractTweetMessage>> GetTimeline(string userId)
        {
            var followingService = (await _grpcChannelService.CreateUserRelationsChannel()).CreateGrpcService<IFollowingService>();
            var followed = await followingService.GetFollowed(userId);

            var tweets = await _mediator.Send(new GetTimelineQuery(userId, followed));
            return tweets.Select(e => new ContractTweetMessage
            {
                Id = e.Id,
                Message = e.Message,
                CreatorId = e.CreatorId,
                PostTime = e.PostTime,
                Likes = e.Likes.Count
            }).ToList();
        }
    }
}