using System;
using System.Reflection;
using IdentityModel.AspNetCore.AccessTokenValidation;
using Kwetter.ApiGateways.WebSpa.Aggregator.Services;
using Kwetter.BuildingBlocks.Abstractions.Services;
using Kwetter.BuildingBlocks.Configurations.Extensions;
using Kwetter.BuildingBlocks.Configurations.Models;
using Kwetter.BuildingBlocks.IdentityBlocks;
using Kwetter.BuildingBlocks.IdentityBlocks.Constants;
using Kwetter.BuildingBlocks.KwetterGrpc;
using Kwetter.BuildingBlocks.KwetterLogger;
using Kwetter.BuildingBlocks.KwetterSwagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Kwetter.ApiGateways.WebSpa.Aggregator
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var identityConfig = services.AddIdentityConfig(Configuration);
            var urlConfig = services.AddUrlConfig(Configuration);

            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

            services.ConfigureKwetterLogger(Configuration);

            services.AddControllers();
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                        .SetIsOriginAllowed(host => true)
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });

            services.AddScopeTransformation();

            // Token management for the api
            services.AddKwetterAccessTokenManagement(identityConfig,
                new[]
                {
                    IdentityKeys.IdentityLocalApiScope,
                    IdentityKeys.TweetApiScope,
                    IdentityKeys.UserRelationsApiScope,
                    IdentityKeys.MediaApiScope
                });

            // Default client
            services.AddClientAccessTokenClient("default-client", "default-client")
                .AddClientAccessTokenHandler();

            // Tweet client
            services.AddClientAccessTokenClient("tweet-client", "default-client",
                configureClient: client => { client.BaseAddress = new Uri(urlConfig.TweetApi); });
            // Ids client
            services.AddClientAccessTokenClient("identity-client", "default-client",
                    configureClient: client => { client.BaseAddress = new Uri(urlConfig.IdentityServerApi); })
                .AddClientAccessTokenHandler();
            // Use token provided by authenticated user
            /* services.AddUserAccessTokenClient("user_client", new UserAccessTokenParameters() { }, client =>
            {
            }); */

            // Grpc
            services.AddTransient<GrpcClientCreatorService>();
            services.AddSingleton<GrpcChannelService>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<ICurrentUserService, CurrentUserService>();
            services.AddTransient<IDateTime, DateTimeService>();

            services.AddTransient<TweetService>();
            services.AddTransient<TimeLineService>();
            services.AddTransient<UserService>();
            services.AddTransient<ProfileService>();
            services.AddTransient<FollowingService>();

            services.AddKwetterIdentity(Environment, identityConfig);
            services.AddKwetterSwagger(Assembly.GetExecutingAssembly(), identityConfig, false);

            services.AddHealthChecks();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IdentityConfig identityConfig)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseKwetterLogger(env);
            app.UseKwetterSwagger("Web Aggregator", identityConfig);

            app.UseRouting();
            app.UseCors("CorsPolicy");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller}/{action=Index}/{id?}");
            });
        }
    }
}