using System;
using System.IO;
using System.Reflection;
using Kwetter.Services.Core.Api.Services;
using Kwetter.Services.Core.Application.Common.Interfaces;
using Kwetter.Services.Core.Application.Common.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Serilog;

namespace Kwetter.Services.Core.Api
{
    public static class StartupExtension
    {
        public static IServiceCollection AddCoreConfigurations(this IServiceCollection services,
            IConfiguration configuration)
        {
            var configUrls = new ConfigUrls();
            configuration.GetSection(ConfigUrls.AppSettingKey).Bind(configUrls);
            services.AddSingleton(s => configUrls);

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services, ConfigIdentity configIdentity, Assembly executingAssembly)
        {
            services.AddSwaggerGen(options =>
            {
                options.EnableAnnotations();
                string xmlFile = $"{executingAssembly.GetName().Name}.xml";
                string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
                
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        /* ClientCredentials = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri($"{globalSettings.IdentityAuthority}/connect/authorize"),
                            TokenUrl = new Uri($"{globalSettings.IdentityAuthority}/connect/token")
                        }, */
                        AuthorizationCode = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri($"{configIdentity.Authority}/connect/authorize"),
                            TokenUrl = new Uri($"{configIdentity.Authority}/connect/token"),
                            Scopes = configIdentity.Scopes
                        }
                    }
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" }
                        },
                        configIdentity.RequiredPolicies
                    }
                });
            });
            return services;
        }

        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            services.AddTransient<ICurrentUserService, CurrentUserService>();
            services.AddHttpContextAccessor();

            return services;
        }

        public static IApplicationBuilder UseLogger(this IApplicationBuilder builder)
        {
            builder.UseSerilogRequestLogging();
            return builder;
        }

        public static IApplicationBuilder UseSwagger(this IApplicationBuilder builder, ConfigIdentity configIdentity, string apiName)
        {
            builder.UseSwagger();
            builder.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", apiName);
                c.OAuthClientSecret(configIdentity.ClientSecret);
                c.OAuthClientId(configIdentity.ClientId);
                c.OAuthAppName($"Kwetter - {apiName} Swagger");
                c.OAuthScopeSeparator(" ");
                c.OAuthUsePkce();
            });
            return builder;
        }
    }
}
