using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Services;
using Kwetter.BuildingBlocks.Abstractions.Exceptions;
using Kwetter.Services.Identity.Api.Infrastructure.Persistence;
using Kwetter.Services.Identity.Api.Models;
using Kwetter.Services.Identity.GrpcContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Kwetter.Services.Identity.Api.Services
{
    // [Authorize(IdentityServerConstants.LocalApi.PolicyName)]
    public class AccountInformationService : IAccountInformationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IProfileService _profileService;

        private readonly IHttpContextAccessor _httpContext;
        private readonly ApplicationDbContext _context;

        public AccountInformationService(UserManager<ApplicationUser> userManager, IProfileService profileService,
            IHttpContextAccessor httpContext, ApplicationDbContext context)
        {
            _userManager = userManager;
            _profileService = profileService;
            _httpContext = httpContext;
            _context = context;
        }

        public async ValueTask<IList<PublicAccount>> SearchAccounts(SearchObject searchObject)
        {
            var accs = await _context.Users
                .Where(e => e.DisplayName.ToLower().Contains(searchObject.SearchTerm.ToLower()) || e.UserName.ToLower().Contains(searchObject.SearchTerm.ToLower()))
                .Take(searchObject.MaxItems)
                .ToListAsync();
            return accs.Select(e => new PublicAccount
            {
                Id = e.Id,
                Username = e.UserName,
                DisplayName = e.DisplayName,
                ProfilePicture = ""
            }).ToList();
        }

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