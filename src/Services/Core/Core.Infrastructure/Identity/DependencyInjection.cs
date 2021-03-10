using IdentityModel;
using IdentityModel.AspNetCore.AccessTokenValidation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Kwetter.Services.Core.Infrastructure.Identity
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddIdentity(this IServiceCollection services, IWebHostEnvironment environment,
            string authority, string clientId, string secret, string[] requiredScopes)
        {
            services.AddAuthentication("token")
                .AddJwtBearer("token", options =>
                {
                    options.Authority = authority;
                    options.TokenValidationParameters.ValidateAudience = false;
                    
                    options.RequireHttpsMetadata = !environment.IsDevelopment() &&
                                                   authority.StartsWith("https");

                    // Also accepting JWT tokens because they don't require a round trip to the identity provider
                    options.TokenValidationParameters.ValidTypes = new[] {"at+jwt", "JWT"};

                    // options.SaveToken = true;
                    // if token does not contain a dot, it is a reference token
                    options.ForwardDefaultSelector = Selector.ForwardReferenceToken("introspection");
                })
                .AddOAuth2Introspection("introspection", options =>
                {
                    options.Authority = authority;

                    options.ClientId = clientId;
                    options.ClientSecret = secret;

                    // options.RoleClaimType = IdentityApiResources.RoleResource;

                    // options.EnableCaching = true;
                    // options.CacheDuration = TimeSpan.FromMinutes(10);
                });


            services.AddAuthorization(config =>
            {
                config.AddPolicy("Application", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    if (requiredScopes != null && requiredScopes.Length > 0) policy.RequireClaim(JwtClaimTypes.Scope, requiredScopes);
                });
            });

            return services;
        }
    }
}
