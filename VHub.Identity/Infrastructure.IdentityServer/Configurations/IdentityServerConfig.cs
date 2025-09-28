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
                Name = "cit_authorization",
                Description = "Доступ к сервисам"
            }
        };


    public static IEnumerable<ApiResource> GetApiResources()
        => new List<ApiResource>()
        {
            new ApiResource("cit_all_access", "Доступ ко всем сервисам")
            {
                Scopes = {"cit_all_access"}
            }
        };

    public static IEnumerable<Client> GetClients()
        => new List<Client>()
        {
            new Client()
            {
                ClientId = "cit_api_client_jwt",
                RequireClientSecret = false,
                AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                AllowedScopes = GetApiResources()
                    .First(f=>f.Name == "cit_all_access").Scopes,
                UpdateAccessTokenClaimsOnRefresh = true,
                AllowOfflineAccess = true,
                RefreshTokenUsage = TokenUsage.OneTimeOnly,
                RefreshTokenExpiration = TokenExpiration.Sliding,
                SlidingRefreshTokenLifetime = 2592000 * 2,
                AccessTokenLifetime = (int) TimeSpan.FromHours(10).TotalSeconds,
            }
        };
}
