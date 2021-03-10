using System.Net.Mime;
using System.Threading.Tasks;
using Kwetter.Services.Core.Api.Common;
using Kwetter.Services.Core.Tweet.Application.TweetMessages.Queries.GetTweetMessageQuery;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace Kwetter.Services.Tweet.Api.Controllers
{
    public class TweetController : ApiControllerBase
    {
        /// <summary>
        /// Get a tweet.
        /// </summary>
        /// <param name="id">The tweet identifier</param>
        /// <returns>A tweet object</returns>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(TweetMessageDto), Status200OK)]
        [ProducesResponseType(Status204NoContent)]
        public async Task<ActionResult<TweetMessageDto>> Get(int id)
        {
            return await Mediator.Send(new GetTweetMessageQuery(id));
        }   
    }
}