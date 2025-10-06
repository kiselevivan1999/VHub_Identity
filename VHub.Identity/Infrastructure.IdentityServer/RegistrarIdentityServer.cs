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

        services.AddIdentity<User, Role>(conf =>
        {
            //Настройки пароля
            conf.Password.RequiredLength = 8;
            conf.Password.RequireNonAlphanumeric = false;
            conf.Password.RequireUppercase = false;
            conf.Password.RequireLowercase = false;
            conf.Password.RequireDigit = false;
            conf.Password.RequiredUniqueChars = 1;

            //Настройки Locout
            conf.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            conf.Lockout.MaxFailedAccessAttempts = 5;
            conf.Lockout.AllowedForNewUsers = true;

            conf.User.RequireUniqueEmail = true;
        })
        .AddDefaultTokenProviders()
        .AddEntityFrameworkStores<AuthDbContext>();
    }
}
