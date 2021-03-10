using System.Reflection;
using FluentValidation.AspNetCore;
using Kwetter.Services.Core.Api;
using Kwetter.Services.Core.Application.Common.Models;
using Kwetter.Services.Core.Tweet.Application;
using Kwetter.Services.Tweet.Api.Filters;
using Kwetter.Services.Tweet.Infrastructure;
using Kwetter.Services.Tweet.Infrastructure.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
            var identityConfig = new ConfigIdentity();
            Configuration.GetSection(ConfigIdentity.AppSettingKey).Bind(identityConfig);
            services.AddSingleton(s => identityConfig);
            
            services.AddCoreConfigurations(Configuration);
            services.AddCoreServices();

            services.AddApplication();
            services.AddInfrastructure(Configuration, Environment, identityConfig);

            services.AddSwagger(identityConfig, Assembly.GetExecutingAssembly());
            services.AddHealthChecks().AddDbContextCheck<ApplicationDbContext>();
            services.Configure<ApiBehaviorOptions>(options =>
            {
                // Suppress default invalid model state behaviour
                options.SuppressModelStateInvalidFilter = true;
            });
            services.AddControllers(options =>
                    options.Filters.Add<ApiExceptionFilterAttribute>())
                .AddFluentValidation();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ConfigIdentity configIdentity)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger(configIdentity, "Tweet Api");
            app.UseLogger();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller}/{action=Index}/{id?}");
            });
        }
    }
}
