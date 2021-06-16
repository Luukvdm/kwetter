using System.Threading.Tasks;
using Kwetter.BuildingBlocks.EventBus.EventBus.Interfaces;
using Kwetter.BuildingBlocks.KwetterGrpc;
using Kwetter.Services.SignalRHub.SignalRHub.Hubs;
using Kwetter.Services.Tweet.Events.Notifications;
using Kwetter.Services.UserRelations.GrpcContracts;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using ProtoBuf.Grpc.Client;

namespace Kwetter.Services.SignalRHub.SignalRHub.EventHandlers
{
    public class TweetCreatedNotificationHandler : IIntegrationEventHandler<TweetCreatedNotification>
    {
        private readonly IHubContext<TweetHub> _tweetHub;
        private readonly ILogger<TweetCreatedNotificationHandler> _logger;
        private readonly GrpcChannelService _grpcChannelService;

        public TweetCreatedNotificationHandler(IHubContext<TweetHub> tweetHub,
            ILogger<TweetCreatedNotificationHandler> logger, GrpcChannelService grpcChannelService)
        {
            _tweetHub = tweetHub;
            _logger = logger;
            _grpcChannelService = grpcChannelService;
        }

        public async Task Handle(TweetCreatedNotification @event)
        {
            _logger.LogInformation("Sending tweet created notification to the client");

            var followingService = (await _grpcChannelService.CreateUserRelationsChannel()).CreateGrpcService<IFollowingService>();
            var receivers = await followingService.GetFollowers(@event.CreatorId);
            receivers.Add(@event.CreatorId);

            await _tweetHub.Clients
                .Groups(receivers)
                .SendAsync("TweetMessageCreated", @event);
        }
    }
}