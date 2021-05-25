using System.Collections.Generic;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using Kwetter.BuildingBlocks.Configurations.Models;
using Kwetter.BuildingBlocks.IdentityBlocks.Constants;

namespace Kwetter.Services.Identity.Api
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new(
                    name: "roles",
                    userClaims: new List<string> {JwtClaimTypes.Role}
                ) {Required = true}
            };

        // TODO: ??
        public static IEnumerable<ApiScope> ApiScopes => new ApiScope[]
        {
            new(IdentityKeys.IdentityLocalApiScope),
            new(IdentityKeys.TweetApiScope),
            new(IdentityKeys.MediaApiScope)
        };

        public static IEnumerable<ApiResource> ApiResources =>
            new ApiResource[]
            {
                new(IdentityServerConstants.LocalApi.ScopeName),
                new(IdentityKeys.TweetApiResource, "Kwetter tweet API")
                {
                    Scopes = new[] {JwtClaimTypes.Role, IdentityKeys.TweetApiScope}
                },
                new(IdentityKeys.WebSpaGatewayResource, "Kwetter WebSpa API")
                {
                    Scopes = new[] {IdentityKeys.TweetApiScope, IdentityKeys.IdentityLocalApiScope}
                },
                new(IdentityKeys.TweetHubResource, "Kweeter tweet Hub")
                {
                    Scopes = new[] {JwtClaimTypes.Role, IdentityKeys.TweetApiScope}
                }
                // new(IdentityKeys.TweetSignalRHubResource, "Kwetter tweet signalr hub")
                // new(MediaApiScope, "Kwetter media API")
            };

        public static IEnumerable<Client> Clients(UrlConfig uris) =>
            new List<Client>
            {
                new()
                {
                    ClientId = "Kwetter.WebSpa",
                    ClientName = "Kwetter SPA web app",
                    AllowedGrantTypes = GrantTypes.Code,
                    AllowAccessTokensViaBrowser = true,
                    RequireClientSecret = false,
                    // RequirePkce = true,
                    // AccessTokenType = AccessTokenType.Reference,

                    ClientUri = uris.WebSpaClient,
                    RedirectUris =
                        {uris.WebSpaClient + "/oidc/callback", uris.WebSpaClient + "/oidc/silent-renew.html"},
                    PostLogoutRedirectUris = {uris.WebSpaClient},
                    AllowedCorsOrigins = {uris.WebSpaClient},

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.LocalApi.ScopeName,
                        IdentityKeys.TweetApiScope,
                        IdentityKeys.TweetSignalRHubResource,
                        IdentityKeys.MediaApiScope
                    }
                },
                new()
                {
                    ClientId = "gateway.webspa",
                    ClientSecrets = {new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes =
                    {
                        IdentityServerConstants.LocalApi.ScopeName,
                        IdentityKeys.TweetApiScope,
                    }
                },
                /* new()
                {
                    ClientId = "Kwetter.TweetApi",
                    ClientSecrets = {new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    Claims = new ClientClaim[]
                    {
                        new ClientClaim("Role", "admin")
                    },
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                    }
                }, */
            };
    }
}