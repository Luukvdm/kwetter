using System.IO.Compression;
using System.Reflection;
using Kwetter.BuildingBlocks.Configurations.Extensions;
using Kwetter.BuildingBlocks.KwetterLogger;
using Kwetter.BuildingBlocks.KwetterSwagger;
using Kwetter.Services.Identity.Api.Infrastructure.Persistence;
using Kwetter.Services.Identity.Api.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ProtoBuf.Grpc.Server;

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
            var urlConfig = services.AddUrlConfig(Configuration);

            services.ConfigureKwetterLogger(Configuration);
            services.AddHttpContextAccessor();
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders =
                    ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });

            services.AddPersistence(Configuration);
            services.AddKwetterIdentityServer(Configuration);
            services.AddLocalApiAuthentication();
            services.AddKwetterSwagger(Assembly.GetExecutingAssembly(), useAnnotations: false);
            
            // Grpc
            services.AddCodeFirstGrpc(config =>
            {
                config.ResponseCompressionLevel = CompressionLevel.Optimal;
                config.EnableDetailedErrors = true;
            });

            services.AddHealthChecks().AddDbContextCheck<ApplicationDbContext>();
            services.AddControllers();
            services.AddControllersWithViews();
            services.AddRazorPages();

            services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            
            string pathBase = Configuration["PATH_BASE"];
            if (!string.IsNullOrEmpty(pathBase))
            {
                logger.LogDebug("Using PATH BASE '{pathBase}'", pathBase);
                app.UsePathBase(pathBase);
            }            
            app.UseForwardedHeaders();
            app.UseMiddleware<PublicFacingUrlMiddleware>();

            app.UseKwetterLogger(env);
            app.UseKwetterSwagger("Identity Server");

            app.UseStaticFiles();
            app.UseHealthChecks("/health");

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseIdentityServer();
            app.UseCookiePolicy(new CookiePolicyOptions {MinimumSameSitePolicy = SameSiteMode.Lax});

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<AccountInformationService>();
                
                // endpoints.MapDefaultControllerRoute();
                endpoints.MapControllers();
                endpoints.MapControllerRoute("default", "{controller}/{action=Index}/{id?}");
                endpoints.MapRazorPages();

                // endpoints.MapControllerRoute("default", "{controller}/{action=Index}/{id?}");
                
                /* endpoints.MapHealthChecks("/hc", new HealthCheckOptions()
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });*/
                endpoints.MapHealthChecks("/health", new HealthCheckOptions
                {
                    Predicate = r => r.Name.Contains("self")
                });
            });
        }
    }
}