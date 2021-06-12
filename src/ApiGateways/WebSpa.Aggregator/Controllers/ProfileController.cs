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
    public class ProfileController : ControllerBase
    {
        private readonly ProfileService _profileService;

        public ProfileController(ProfileService profileService)
        {
            _profileService = profileService;
        }
        
        [HttpGet("{username}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Profile), Status200OK)]
        [ProducesResponseType(Status204NoContent)]
        public async Task<Profile> GetProfile(string username)
        {
            return await _profileService.GetProfile(username);
        }
        
        [HttpGet("me")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Profile), Status200OK)]
        [ProducesResponseType(Status204NoContent)]
        public async Task<Profile> GetMyProfile()
        {
            return await _profileService.GetMyProfile();
        }
    }
}