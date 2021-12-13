using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Kwetter.BuildingBlocks.Configurations.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Kwetter.BuildingBlocks.KwetterSwagger
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddKwetterSwagger(this IServiceCollection services,
            Assembly executingAssembly,
            IdentityConfig configIdentity = null,
            bool useAnnotations = true)
        {
            services.AddSwaggerGen(options =>
            {
                if (useAnnotations)
                {
                    options.EnableAnnotations();
                    string xmlFile = $"{executingAssembly.GetName().Name}.xml";
                    string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                    options.IncludeXmlComments(xmlPath);
                }

                if (configIdentity != null)
                {
                    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                    {
                        Type = SecuritySchemeType.OAuth2,
                        Flows = new OpenApiOAuthFlows
                        {
                            ClientCredentials = new OpenApiOAuthFlow
                            {
                                AuthorizationUrl = new Uri($"{configIdentity.Authority}/connect/authorize"),
                                TokenUrl = new Uri($"{configIdentity.Authority}/connect/token"),
                                Scopes = new Dictionary<string, string> { {"IdentityServerApi", ""}, {"tweet.api", ""}, {"userrelations.api", ""}, {"media.api",""} , {"hub", ""} }
                            }
                        }
                    });

                    options.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference {Type = ReferenceType.SecurityScheme, Id = "oauth2"}
                            },
                            new List<string>()
                        }
                    });
                }
            });
            
            return services;
        }

        public static IApplicationBuilder UseKwetterSwagger(this IApplicationBuilder builder,
            string apiName,
            IdentityConfig configIdentity = null)
        {
            builder.UseSwagger();
            builder.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", apiName);
                //c.OAuthClientSecret(configIdentity.ClientSecret);
                if (configIdentity != null) c.OAuthClientId(configIdentity.ClientId);
                c.OAuthAppName($"Kwetter - {apiName} Swagger");
                c.OAuthScopeSeparator(" ");
                c.OAuthUsePkce();
            });
            return builder;
        }
    }
}