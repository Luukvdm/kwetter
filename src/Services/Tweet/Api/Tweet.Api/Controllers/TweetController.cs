using System.Net.Mime;
using Kwetter.BuildingBlocks.Abstractions.Services;
using Kwetter.BuildingBlocks.EventBus.EventBus.Interfaces;
using Kwetter.Services.Tweet.Api.Controllers.Dtos;
using Kwetter.Services.Tweet.Events.Events;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace Kwetter.Services.Tweet.Api.Controllers
{
    [Authorize]
    public class TweetController : ApiControllerBase
    {
        private readonly IEventBus _eventBus;
        private readonly ICurrentUserService _currentUser;
        private readonly IDateTime _dateTime;

        public TweetController(IEventBus eventBus, ICurrentUserService currentUser, IDateTime dateTime)
        {
            _eventBus = eventBus;
            _currentUser = currentUser;
            _dateTime = dateTime;
        }

        /// <summary>
        /// Post a tweet
        /// </summary>
        /// <param name="tweet">Tweet message object</param>
        /// <returns>Post result</returns>
        [HttpPost]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(Status200OK)]
        public ActionResult Create([FromBody] CreateTweetMessageDto tweet)
        {
            _eventBus.Publish(new CreateTweetMessageEvent(tweet.Message, _currentUser.UserId, _dateTime.Now));
            return Ok();
        }
    }
}