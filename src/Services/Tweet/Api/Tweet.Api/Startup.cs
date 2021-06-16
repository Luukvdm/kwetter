using System.IO.Compression;
using System.Reflection;
using FluentValidation.AspNetCore;
using Kwetter.BuildingBlocks.Abstractions.Services;
using Kwetter.BuildingBlocks.Configurations.Extensions;
using Kwetter.BuildingBlocks.Configurations.Models;
using Kwetter.BuildingBlocks.EventBus.EventBus.Interfaces;
using Kwetter.BuildingBlocks.IdentityBlocks;
using Kwetter.BuildingBlocks.IdentityBlocks.Constants;
using Kwetter.BuildingBlocks.KwetterGrpc;
using Kwetter.BuildingBlocks.KwetterLogger;
using Kwetter.BuildingBlocks.KwetterSwagger;
using Kwetter.Services.Tweet.Api.Filters;
using Kwetter.Services.Tweet.Api.GrpcServices;
using Kwetter.Services.Tweet.Application;
using Kwetter.Services.Tweet.Infrastructure;
using Kwetter.Services.Tweet.Infrastructure.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ProtoBuf.Grpc.Server;

namespace Kwetter.Services.Tweet.Api
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
            var eventBusConfig = services.AddEventBusConfig(Configuration);

            services.AddApplication();
            services.AddInfrastructure(Configuration);
            
            services.AddHttpContextAccessor();
            services.ConfigureKwetterLogger(Configuration);
            services.AddTransient<ICurrentUserService, CurrentUserService>();
            services.AddEventBus(eventBusConfig);
            services.AddKwetterIdentity(Environment, identityConfig);
            
            services.AddHealthChecks().AddDbContextCheck<ApplicationDbContext>();
            services.AddKwetterSwagger(Assembly.GetExecutingAssembly(), identityConfig);
            services.Configure<ApiBehaviorOptions>(options =>
            {
                // Suppress default invalid model state behaviour
                options.SuppressModelStateInvalidFilter = true;
            });

            // Http client setup with access token
            const string accessTokenClientName = "default-toke-client";
            const string httpClientName = "default-client";
            services.AddKwetterAccessTokenManagement(accessTokenClientName, identityConfig,
                new[]
                {
                    IdentityKeys.UserRelationsApiScope
                });
            services.AddKwetterAuthorizedHttpClients(httpClientName, accessTokenClientName, false, httpOptions => { });
            
            // Grpc service
            services.AddCodeFirstGrpc(config =>
            {
                config.ResponseCompressionLevel = CompressionLevel.Optimal;
            });
            // Grpc client
            services.AddGrpcClientServices(accessTokenClientName, httpClientName);
            
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                        .SetIsOriginAllowed(_ => true)
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });

            services.AddControllers(options =>
                    options.Filters.Add<ApiExceptionFilterAttribute>())
                .AddFluentValidation();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IEventBus eventBus, IdentityConfig identityConfig, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseKwetterLogger(env);
            app.UseKwetterSwagger("Tweet Processor", identityConfig);

            EventBusConfigurator.ConfigureHandlers(eventBus);

            app.UseRouting();
            app.UseCors("CorsPolicy");
            
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<TimelineService>();
                endpoints.MapGrpcService<TweetService>();
                
                endpoints.MapControllerRoute("default", "{controller}/{action=Index}/{id?}");
            });
        }
    }
}