using System.Collections.Generic;
using System.Security.Claims;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using Kwetter.Services.Core.Application.Common.Models;

namespace Kwetter.Services.Identity.Api
{
    public static class Config
    {
        private const string TweetApiScope = "tweets";
        private const string MediaApiScope = "media";

        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(), new IdentityResources.Profile(),
                new IdentityResources.Email()
                /* new(name: RolesScope,
                    displayName: "Roles",
                    userClaims: new List<string> {JwtClaimTypes.Role}
                )
                {
                    Required = true
                } */
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[] {new(TweetApiScope, "Kwetter Api scope")};

        public static IEnumerable<ApiResource> ApiResources =>
            new ApiResource[]
            {
                new(TweetApiScope, "Kwetter tweet API"),
                new(MediaApiScope, "Kwetter media API")
            };

        public static IEnumerable<Client> Clients(ConfigUrls uris) =>
            new List<Client>
            {
                new()
                {
                    ClientId = "Kwetter.WebSpa",
                    ClientName = "Kwetter SPA web app",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireClientSecret = false,
                    AllowAccessTokensViaBrowser = true,
                    RequirePkce = true,
                    // AccessTokenType = AccessTokenType.Reference,
                    // AlwaysIncludeUserClaimsInIdToken = true, -> Is nuttig om extra claims in token te krijgen

                    ClientUri = uris.WebSpaClient,
                    RedirectUris =
                        {uris.WebSpaClient + "/oidc/callback", uris.WebSpaClient + "/oidc/silent-renew.html"},
                    PostLogoutRedirectUris = {uris.WebSpaClient},
                    AllowedCorsOrigins = {uris.WebSpaClient},
                    
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        TweetApiScope,
                        MediaApiScope
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