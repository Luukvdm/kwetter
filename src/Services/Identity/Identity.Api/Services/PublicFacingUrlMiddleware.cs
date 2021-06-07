using System;
using System.Threading.Tasks;
using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Kwetter.Services.Identity.Api.Services
{
    /// <summary>
    /// Configures the HttpContext by assigning IdentityServerOrigin.
    /// </summary>
    public class PublicFacingUrlMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public PublicFacingUrlMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task Invoke(HttpContext context)
        {
            string pathBase = _configuration["PATH_BASE"];
            string urlBase = _configuration["URL_BASE"];

            if (!string.IsNullOrEmpty(urlBase)) context.SetIdentityServerOrigin(urlBase);
            if (!string.IsNullOrEmpty(pathBase))
            {
                if (!pathBase.StartsWith('/')) pathBase = "/" + pathBase;
                context.SetIdentityServerBasePath(pathBase /*request.PathBase.Value.TrimEnd('/')*/);
            }

            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw e;
            }
        }
    }
}