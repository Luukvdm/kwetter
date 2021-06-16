using System.Threading.Tasks;
using Kwetter.ApiGateways.WebSpa.Aggregator.Services;
using Kwetter.BuildingBlocks.Abstractions.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace Kwetter.ApiGateways.WebSpa.Aggregator.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class UserRelationsController : ControllerBase
    {
        private readonly ICurrentUserService _currentUser;
        private readonly FollowingService _followingService;

        public UserRelationsController(FollowingService followingService, ICurrentUserService currentUser)
        {
            _followingService = followingService;
            _currentUser = currentUser;
        }

        /// <summary>
        /// Follow a user
        /// </summary>
        /// <param name="userId">The user you want to follow</param>
        [HttpPost("follow/{userId}")]
        [ProducesResponseType(Status201Created)]
        public async Task Follow([FromRoute] string userId)
        {
            await _followingService.CreateFollowing(_currentUser.UserId, userId);
        }
        
        /// <summary>
        /// Unfollow a user
        /// </summary>
        /// <param name="userId">The user you want to unfollow</param>
        [HttpPost("unfollow/{userId}")]
        [ProducesResponseType(Status201Created)]
        public async Task UnFollow([FromRoute] string userId)
        {
            await _followingService.RemoveFollowing(_currentUser.UserId, userId);
        }
    }
}