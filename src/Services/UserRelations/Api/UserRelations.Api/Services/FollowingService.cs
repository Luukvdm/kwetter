using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kwetter.BuildingBlocks.Abstractions.Services;
using Kwetter.BuildingBlocks.EventBus.EventBus.Interfaces;
using Kwetter.Services.UserRelations.Application.Queries.GetFollowedUserIds;
using Kwetter.Services.UserRelations.Events.Events;
using Kwetter.Services.UserRelations.GrpcContracts;
using MediatR;

namespace Kwetter.Services.UserRelations.Api.Services
{
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
        /// Get all user ids followed by this user
        /// </summary>
        /// <param name="userId">Id of the following user</param>
        /// <returns>List with user ids followed by the user</returns>
        public async ValueTask<IList<string>> GetFollowedUserIds(string userId)
        {
            var followings = await _mediator.Send(new GetFollowedUserIdsQuery(userId));
            return followings.Select(e => e.FollowedUserId).ToList();
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
    }
}