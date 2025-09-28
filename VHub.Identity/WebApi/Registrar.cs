using Application.Abstracts.Services;
using Application.Implementations.Services;
using Infrastructure.IdentityServer;
using Infrastructure.EntityFramework;

namespace WebApi;

public static class Registrar
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {

        services.InitializeIdentityServer(configuration);
        services.InitializeDb(configuration);
        return services;
    }

    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        return services;
    }


}
