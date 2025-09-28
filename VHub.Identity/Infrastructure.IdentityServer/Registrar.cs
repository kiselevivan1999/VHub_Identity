using Domain.Entities;
using Infrastructure.IdentityServer.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.IdentityServer;

public static class Registrar
{
    public static void InitializeIdentityServer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddIdentityServer(conf =>
        {
            conf.Events.RaiseErrorEvents = true;
            conf.Events.RaiseFailureEvents = true;
            conf.Events.RaiseSuccessEvents = true;
            conf.Authentication.CookieSlidingExpiration = true;
        })
            .AddAspNetIdentity<User>()
            .AddInMemoryIdentityResources(IdentityServerConfig.GetIdentityResources())
            .AddInMemoryApiScopes(IdentityServerConfig.GetApiScopes())
            .AddInMemoryIdentityResources(IdentityServerConfig.GetIdentityResources())
            .AddInMemoryClients(IdentityServerConfig.GetClients())
            .AddDeveloperSigningCredential();
    }
}
