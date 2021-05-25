using System;
using System.Threading.Tasks;
using Kwetter.BuildingBlocks.Abstractions.Services;
using Microsoft.AspNetCore.Authorization;

namespace Kwetter.Services.Tweet.Hub
{
    [Authorize]
    public class TweetHub : Microsoft.AspNetCore.SignalR.Hub
    {
        private readonly ICurrentUserService _currentUser;

        public TweetHub(ICurrentUserService currentUser)
        {
            _currentUser = currentUser;
        }

        /// <inheritdoc />
        public override async Task OnConnectedAsync()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, _currentUser.UserId);
            await base.OnConnectedAsync();
        }

        /// <inheritdoc />
        public override async Task OnDisconnectedAsync(Exception ex)
        {
            Console.WriteLine(_currentUser.UserId + " disconnected!");
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, _currentUser.UserId);
            await base.OnDisconnectedAsync(ex);
        }
    }
}