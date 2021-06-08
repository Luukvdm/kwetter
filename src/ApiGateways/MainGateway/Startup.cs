using Kwetter.BuildingBlocks.Configurations.Extensions;
using Kwetter.BuildingBlocks.Configurations.Models;
using Kwetter.BuildingBlocks.KwetterLogger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace Kwetter.ApiGateways.MainGateway
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
            services.AddControllers();
            var urlConfig = services.AddUrlConfig(Configuration);
            services.ConfigureKwetterLogger(Configuration);
            services.AddCors();
            
            services.AddOcelot();
            services.AddSwaggerForOcelot(Configuration, swaggerSetup: options =>
            {
                // options.DescribeAllEnumsAsStrings();
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Kwetter Gateway Swagger Doc",
                    Version = "v1",
                    Description = "The Kwetter Swagger"
                });

                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        /* AuthorizationCode = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri($"{configUrls.IdentityServer}/connect/authorize"),
                            TokenUrl = new Uri($"{configUrls.IdentityServer}/connect/token"),
                            // Scopes = { new KeyValuePair<string, string>(IdentityKeys.EnvApiScope, "") }
                        } */
                    }
                });

                /* options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference {Type = ReferenceType.SecurityScheme, Id = "oauth2"}
                        },
                        new[]
                        {
                            IdentityKeys.RolesScope, 
                            IdentityKeys.EnvApiScope, 
                            IdentityKeys.ProdApiScope,
                            IdentityKeys.UsageApiScope
                        }
                    }
                }); */
            });
        }

        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env, UrlConfig urlConfig)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCors(x => x
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .SetIsOriginAllowed(origin => true) // allow any origin
                    .AllowCredentials()); // allow credentials
            }
            else
            {
                app.UseCors(x => x
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .SetIsOriginAllowed(origin => origin == urlConfig.WebSpaClient)
                    .AllowCredentials());
            }

            app.UseKwetterLogger(env);
            
            app.UseSwaggerForOcelotUI(opt =>
            {
                opt.PathToSwaggerGenerator = "/swagger/docs";
                // opt.OAuthClientId(IdentityKeys.GatewaySwaggerId);
                opt.OAuthAppName("Kwetter - Gateway Swagger");
                opt.OAuthScopeSeparator(" ");
                // opt.OAuthUsePkce();
            });
            
            app.UseWebSockets();
            await app.UseOcelot();
        }
    }
}