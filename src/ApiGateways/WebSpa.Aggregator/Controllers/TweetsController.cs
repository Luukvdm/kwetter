using System.Net.Mime;
using System.Threading.Tasks;
using Kwetter.ApiGateways.WebSpa.Aggregator.Controllers.Dtos;
using Kwetter.ApiGateways.WebSpa.Aggregator.Models;
using Kwetter.ApiGateways.WebSpa.Aggregator.Services;
using Kwetter.BuildingBlocks.Abstractions.Services;
using Kwetter.Services.Tweet.GrpcContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace Kwetter.ApiGateways.WebSpa.Aggregator.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class TweetsController : ControllerBase
    {
        private readonly ICurrentUserService _currentUser;
        private readonly IDateTime _dateTime;

        private readonly TimeLineService _timeLineService;
        private readonly TweetService _tweetService;

        public TweetsController(ICurrentUserService currentUser, IDateTime dateTime, TimeLineService timeLineService,
            TweetService tweetService)
        {
            _currentUser = currentUser;
            _dateTime = dateTime;

            _timeLineService = timeLineService;
            _tweetService = tweetService;
        }

        [HttpGet("timeline")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(TimeLine), Status200OK)]
        [ProducesResponseType(Status204NoContent)]
        public async Task<TimeLine> GetTimeLine()
        {
            return await _timeLineService.GetTimeLine(_currentUser.UserId);
        }

        /// <summary>
        /// Get a tweet by its ID
        /// </summary>
        /// <param name="id">Id of the tweet</param>
        /// <returns>A tweet object</returns>
        [HttpGet("{id:int}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Tweet), Status200OK)]
        [ProducesResponseType(Status204NoContent)]
        public async Task<Tweet> Get(int id)
        {
            return await _tweetService.Get(id);
        }

        /// <summary>
        /// Post a tweet
        /// </summary>
        /// <param name="tweet">Tweet message object</param>
        /// <returns>Post result</returns>
        [HttpPost]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(Status201Created)]
        public async Task Create([FromBody] CreateTweetVm tweet)
        {
            var tweetObject = new CreateTweet
            {
                Message = tweet.Message,
                UserId = _currentUser.UserId,
                PostTime = _dateTime.Now
            };
            await _tweetService.Create(tweetObject);
        }

        /// <summary>
        /// Like a tweet
        /// </summary>
        /// <param name="tweetId">Id of the tweet</param>
        [HttpPost("like/{tweetId:int}")]
        [ProducesResponseType(Status204NoContent)]
        [ProducesResponseType(Status201Created)]
        public async Task Like([FromRoute] int tweetId)
        {
            var likeObject = new CreateLike
            {
                LikeTime = _dateTime.Now,
                TweetId = tweetId,
                UserId = _currentUser.UserId
            };
            await _tweetService.Like(likeObject);
        }
    }
}