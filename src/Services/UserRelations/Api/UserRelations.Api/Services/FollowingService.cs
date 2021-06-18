using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kwetter.BuildingBlocks.Abstractions.Services;
using Kwetter.BuildingBlocks.EventBus.EventBus.Interfaces;
using Kwetter.Services.UserRelations.Application.Queries.GetFollowed;
using Kwetter.Services.UserRelations.Application.Queries.GetFollowers;
using Kwetter.Services.UserRelations.Events.Events;
using Kwetter.Services.UserRelations.GrpcContracts;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace Kwetter.Services.UserRelations.Api.Services
{
    [Authorize]
    public class FollowingService : IFollowingService
    {
        private readonly IEventBus _eventBus;
        private readonly ISender _mediator;
        private readonly IDateTime _dateTime;

        public FollowingService(IEventBus eventBus, ISender mediator, IDateTime dateTime)
        {
            _eventBus = eventBus;
            _mediator = mediator;
            _dateTime = dateTime;
        }

        /// <summary>
        /// Get all ids from users followed by this user
        /// </summary>
        /// <param name="userId">Id of the following user</param>
        /// <returns>List with user ids</returns>
        public async ValueTask<IList<string>> GetFollowed(string userId)
        {
            var followings = await _mediator.Send(new GetFollowedQuery(userId));
            return followings.Select(e => e.FollowedUserId).ToList();
        }

        /// <summary>
        /// Get all ids from users that follow this user
        /// </summary>
        /// <param name="userId">Id of the followed user</param>
        /// <returns>List with user ids</returns>
        public async ValueTask<IList<string>> GetFollowers(string userId)
        {
            var followers = await _mediator.Send(new GetFollowersQuery(userId));
            return followers.Select(e => e.FollowingUserId).ToList();
        }

        /// <summary>
        /// Create a new following
        /// </summary>
        /// <param name="following">Following object to be created</param>
        /// <returns>A task</returns>
        public ValueTask CreateFollowing(CreateFollowing following)
        {
            _eventBus.Publish(new CreateFollowEvent(following.FollowingUserId, following.FollowedUserId,
                _dateTime.Now));
            return ValueTask.CompletedTask;
        }

        public ValueTask RemoveFollowing(RemoveFollowing following)
        {
            _eventBus.Publish(new RemoveFollowEvent(following.FollowingUserId, following.FollowedUserId));
            return ValueTask.CompletedTask;
        }

        /// <summary>
        /// Remove a following
        /// </summary>
        /// <param name="followerUserId">Id of the followed user</param>
        /// <param name="followingUserId">Id of the following user</param>
        /// <returns>A task</returns>
        public ValueTask RemoveFollowing(string followerUserId, string followingUserId)
        {
            _eventBus.Publish(new RemoveFollowEvent(followerUserId, followingUserId));
            return ValueTask.CompletedTask;
        }
    }
}