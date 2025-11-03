using Domain.Entities;
using Infrastructure.EntityFramework.Contexts;
using Infrastructure.IdentityServer.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.IdentityServer;

public static class RegistrarIdentityServer
{
    public static void InitializeIdentityServer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddIdentity<User, Role>(conf =>
        {
            //Настройки пароля
            conf.Password.RequiredLength = 8;
            conf.Password.RequireNonAlphanumeric = false;
            conf.Password.RequireUppercase = true;
            conf.Password.RequireLowercase = true;
            conf.Password.RequireDigit = true;
            conf.Password.RequiredUniqueChars = 2;

            conf.User.RequireUniqueEmail = true;
        })
        .AddDefaultTokenProviders()
        .AddErrorDescriber<IdentityServerErrorDescriber>()
        .AddEntityFrameworkStores<AuthDbContext>();

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
            .AddProfileService<IdentityServerProfileService>()
            .AddDeveloperSigningCredential();
    }
}
