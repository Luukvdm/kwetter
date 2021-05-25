using System.Security.Claims;
using IdentityModel;
using Kwetter.BuildingBlocks.Abstractions.Services;
using Microsoft.AspNetCore.Http;

namespace Kwetter.BuildingBlocks.IdentityBlocks
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        
        public bool IsAuthenticated => _httpContextAccessor.HttpContext?.User.Identity != null &&
                                       _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;

        public string UserId => _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        public string Name => _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Name);

        public string UserName =>
            _httpContextAccessor.HttpContext?.User.FindFirstValue(JwtClaimTypes.PreferredUserName);

        public string Role => _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Role);
    }
}
