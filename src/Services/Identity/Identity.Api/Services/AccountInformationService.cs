using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityServer4.Services;
using Kwetter.BuildingBlocks.Abstractions.Exceptions;
using Kwetter.Services.Identity.Api.Models;
using Kwetter.Services.Identity.GrpcContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Kwetter.Services.Identity.Api.Services
{
    // [Authorize(IdentityServerConstants.LocalApi.PolicyName)]
    public class AccountInformationService : IAccountInformationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IProfileService _profileService;

        private readonly IHttpContextAccessor _httpContext;

        public AccountInformationService(UserManager<ApplicationUser> userManager, IProfileService profileService,
            IHttpContextAccessor httpContext)
        {
            _userManager = userManager;
            _profileService = profileService;
            _httpContext = httpContext;
        }

        // [Authorize(IdentityServerConstants.LocalApi.PolicyName)]
        public async ValueTask<PublicAccount> GetAccount(string userId)
        {
            var acc = await _userManager.FindByIdAsync(userId);
            if (acc == null) throw new NotFoundException("Unknown user");
            var account = new PublicAccount
            {
                Id = acc.Id,
                DisplayName = acc.DisplayName,
                Username = acc.UserName
            };
            return account;
        }

        public async ValueTask<PublicAccount> GetAccountByUsername(string username)
        {
            var acc = await _userManager.FindByNameAsync(username);
            if (acc == null) throw new NotFoundException("Unknown user");
            var account = new PublicAccount
            {
                Id = acc.Id,
                DisplayName = acc.DisplayName,
                Username = acc.UserName
            };
            return account;
        }

        public async ValueTask<IList<PublicAccount>> GetAccounts(string[] userIds)
        {
            var accounts = new List<PublicAccount>();

            foreach (string userId in userIds)
            {
                var acc = await _userManager.FindByIdAsync(userId);
                if (acc == null)
                {
                    var empty = new PublicAccount
                    {
                        Id = userId,
                        DisplayName = "[Unavailable]",
                        Username = "[Unavailable]"
                    };
                    accounts.Add(empty);
                    continue;
                }

                var dto = new PublicAccount
                {
                    Id = acc.Id,
                    DisplayName = acc.DisplayName,
                    Username = acc.UserName
                };
                accounts.Add(dto);
            }

            return accounts;
        }
    }
}