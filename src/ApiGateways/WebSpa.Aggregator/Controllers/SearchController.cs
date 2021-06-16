using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using Kwetter.ApiGateways.WebSpa.Aggregator.Models;
using Kwetter.ApiGateways.WebSpa.Aggregator.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace Kwetter.ApiGateways.WebSpa.Aggregator.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class SearchController : ControllerBase
    {
        private readonly UserService _userService;

        public SearchController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet("users")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(IList<User>), Status200OK)]
        public async Task<IList<User>> Search([FromQuery] string search)
        {
            return await _userService.SearchUsers(search);
        }

    }
}