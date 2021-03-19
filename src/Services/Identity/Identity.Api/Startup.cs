using Kwetter.Services.Core.Api;
using Kwetter.Services.Core.Application.Common.Models;
using Kwetter.Services.Identity.Api.Infrastructure;
using Kwetter.Services.Identity.Api.Infrastructure.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Kwetter.Services.Identity.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCoreConfigurations(Configuration);
            services.AddCoreServices();

            services.AddControllersWithViews();
            
            services.AddPersistence(Configuration);
            var configUrls = services.BuildServiceProvider().GetService<ConfigUrls>();
            services.AddIdentityServer(Configuration, configUrls);

            services.AddHealthChecks().AddDbContextCheck<ApplicationDbContext>();
            services.AddRazorPages();

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseLogger();

            app.UseHealthChecks("/health");
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseIdentityServer();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
