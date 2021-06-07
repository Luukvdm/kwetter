using System.Threading.Tasks;
using IdentityServer4.Services;
using Kwetter.BuildingBlocks.Configurations.Models;
using Kwetter.Services.Identity.Api.Models;
using Kwetter.Services.Identity.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kwetter.Services.Identity.Api.Controllers
{
    public class HomeController : Controller
    {
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IRedirectService _redirectSvc;
        private readonly UrlConfig _urlConfig;

        public HomeController(IIdentityServerInteractionService interaction, IRedirectService redirectSvc, UrlConfig urlConfig)
        {
            _interaction = interaction;
            _redirectSvc = redirectSvc;
            _urlConfig = urlConfig;
        }

        public IActionResult Index(string returnUrl)
        {
            ViewData["KwetterUrl"] = _urlConfig.WebSpaClient;
            return View();
        }

        public IActionResult ReturnToOriginalApplication(string returnUrl)
        {
            if (returnUrl != null)
                return Redirect(_redirectSvc.ExtractRedirectUriFromReturnUrl(returnUrl));
            else
                return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Shows the error page
        /// </summary>
        public async Task<IActionResult> Error(string errorId)
        {
            var vm = new ErrorViewModel();

            // retrieve error details from identityserver
            var message = await _interaction.GetErrorContextAsync(errorId);
            if (message != null)
            {
                vm.Error = message;
            }

            return View("Error", vm);
        }
    }
}