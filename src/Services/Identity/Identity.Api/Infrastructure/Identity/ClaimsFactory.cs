using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using Kwetter.Services.Identity.Api.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Kwetter.Services.Identity.Api.Infrastructure.Identity
{
    public class ClaimsFactory : UserClaimsPrincipalFactory<IdentityUser>
    {
        private readonly IApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ClaimsFactory(
            UserManager<IdentityUser> userManager,
            IOptions<IdentityOptions> optionsAccessor,
            IApplicationDbContext context) : base(userManager, optionsAccessor)
        {
            _context = context;
            _userManager = userManager;
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(IdentityUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            
            identity.AddClaims(roles.Select(role => new Claim(JwtClaimTypes.Role, role)));
            
            return identity;
        }
    }
}