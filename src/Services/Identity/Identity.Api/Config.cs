using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;
using Kwetter.Services.Core.Application.Common.Models;

namespace Kwetter.Services.Identity.Api
{
    public static class Config
    {
        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[] {new("someScope", "Some scope")};

        public static IEnumerable<ApiResource> ApiResources =>
            new ApiResource[] {new("someApi", "Some API")};
            
        public static IEnumerable<Client> Clients(ConfigUrls uris) =>
            new List<Client>
            {
                // Energy Grid Web Client
                new()
                {
                    ClientId = "EnergyGrid.WebClient",
                    ClientName = "EnergyGrid.WebClient",
                    ClientSecrets = {new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireClientSecret = false,
                    RequirePkce = true,
                    AllowAccessTokensViaBrowser = true,
                    // AlwaysIncludeUserClaimsInIdToken = true, -> Is nuttig om extra claims in token te krijgen

                    ClientUri = uris.WebSpaClient,
                    RedirectUris = {uris.WebSpaClient + "/oidc/callback", uris.WebSpaClient + "/oidc/silent-renew.html"},
                    PostLogoutRedirectUris = {uris.WebSpaClient},
                    AllowedCorsOrigins = {uris.WebSpaClient},
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                    }
                },
                new()
                {
                    ClientId = "EnergyGrid.EnergyProductionApi",
                    ClientSecrets = {new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                    }
                },
            };
    }
}
