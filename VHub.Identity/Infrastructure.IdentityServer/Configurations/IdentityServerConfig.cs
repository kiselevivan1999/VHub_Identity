using Duende.IdentityServer.Models;

namespace Infrastructure.IdentityServer.Configurations;

internal static class IdentityServerConfig
{
    public static IEnumerable<IdentityResource> GetIdentityResources()
        => new List<IdentityResource>()
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };

    public static IEnumerable<ApiScope> GetApiScopes()
        => new List<ApiScope>()
        {
            new ApiScope()
            {
                Name = "vhub_authorization",
                Description = "Доступ к сервисам"
            }
        };


    public static IEnumerable<ApiResource> GetApiResources()
        => new List<ApiResource>()
        {
            new ApiResource("vhub_all_access", "Доступ ко всем сервисам")
            {
                Scopes = { "vhub_authorization" }
            }
        };

    public static IEnumerable<Client> GetClients()
    { 
        var result = new List<Client>()
        {
            new Client()
            {
                ClientId = "vhub_api_client_jwt",
                RequireClientSecret = false,
                AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                AllowedScopes = { "vhub_authorization" },
                //AllowedScopes = GetApiResources()
                //    .First(f=>f.Name == "vhub_all_access").Scopes,
                UpdateAccessTokenClaimsOnRefresh = true,
                AllowOfflineAccess = true,
                RefreshTokenUsage = TokenUsage.OneTimeOnly,
                RefreshTokenExpiration = TokenExpiration.Sliding,
                SlidingRefreshTokenLifetime = 2592000 * 2,
                AccessTokenLifetime = (int) TimeSpan.FromHours(10).TotalSeconds,
            }
        };

        return result;

    }
   
}
