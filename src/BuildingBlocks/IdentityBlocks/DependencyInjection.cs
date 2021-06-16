using System;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using IdentityModel.AspNetCore.AccessTokenManagement;
using IdentityModel.AspNetCore.AccessTokenValidation;
using IdentityModel.Client;
using Kwetter.BuildingBlocks.Configurations.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Kwetter.BuildingBlocks.IdentityBlocks
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddKwetterIdentity(this IServiceCollection services,
            IWebHostEnvironment environment,
            IdentityConfig identityConfig)
        {
            services.AddAuthentication("token")
                .AddJwtBearer("token", options =>
                {
                    options.Authority = identityConfig.Authority;

                    options.RequireHttpsMetadata = !environment.IsDevelopment() &&
                                                   identityConfig.Authority.StartsWith("https");

                    // options.TokenValidationParameters.ValidateAudience = false;
                    options.Audience = identityConfig.RequiredAudience;

                    options.TokenValidationParameters.ValidTypes = new[] {"at+jwt"};

                    // options.SaveToken = true;
                    // if token does not contain a dot, it is a reference token
                    options.ForwardDefaultSelector = Selector.ForwardReferenceToken("introspection");

                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var accessToken = context.Request.Query["access_token"];

                            // If the request is for a hub
                            var path = context.HttpContext.Request.Path;
                            if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/hub"))
                            {
                                context.Token = accessToken;
                            }

                            return Task.CompletedTask;
                        }
                    };
                })
                .AddOAuth2Introspection("introspection", options =>
                {
                    options.Authority = identityConfig.Authority;

                    options.ClientId = identityConfig.ClientId;
                    options.ClientSecret = identityConfig.ClientSecret;

                    options.RoleClaimType = JwtClaimTypes.Role;
                    options.NameClaimType = JwtClaimTypes.Name;

                    // options.EnableCaching = true;
                    // options.CacheDuration = TimeSpan.FromMinutes(10);
                });

            services.AddScopeTransformation();

            services.AddAuthorization(config =>
            {
                config.FallbackPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                /* config.AddPolicy("AuthStuff", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    // policy.RequireClaim(ClaimTypes.Role);
                    foreach (string scope in requiredScopes)
                    {
                        policy.RequireClaim(scope);
                    }
                }); */
            });

            return services;
        }

        public static IServiceCollection AddKwetterAccessTokenManagement(this IServiceCollection services,
            string clientName,
            IdentityConfig identityConfig, string[] scopes)
        {
            services.AddScopeTransformation();
            services.AddAccessTokenManagement(options =>
            {
                string strScopes = string.Join(' ', scopes);

                options.Client.Clients.Add(clientName,
                    new ClientCredentialsTokenRequest
                    {
                        Address = identityConfig.Authority + "/connect/token",
                        ClientId = identityConfig.ClientId,
                        ClientSecret = identityConfig.ClientSecret,
                        Scope = strScopes
                    });
                options.User.Scheme = JwtBearerDefaults.AuthenticationScheme;
            });


            return services;
        }

        public static IServiceCollection AddKwetterAuthorizedHttpClients(this IServiceCollection services, string name,
            string tokenClientName, bool reuseClientToken, Action<HttpClient> configureClient)
        {
            if (reuseClientToken)
            {
                services.AddUserAccessTokenClient(name, new UserAccessTokenParameters() { }, configureClient);
            }
            else
            {
                services.AddClientAccessTokenClient(name, tokenClientName, configureClient: configureClient)
                    .AddClientAccessTokenHandler();
            }

            return services;
        }
    }
}