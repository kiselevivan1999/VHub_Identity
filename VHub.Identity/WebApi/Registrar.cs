using Application.Abstracts.Services;
using Application.Implementations.Services;
using Infrastructure.IdentityServer;
using Infrastructure.EntityFramework;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.OpenApi.Models;

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

    public static IServiceCollection AddAuthenticationAndAuthorizationService(this IServiceCollection services,
    IConfiguration configuration)
    {
        string authorizationIdentityServerUri = configuration.GetValue<string>("IdentityUrlName")!;

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
            JwtBearerDefaults.AuthenticationScheme, conf =>
            {
                conf.Authority = authorizationIdentityServerUri;
                conf.Audience = authorizationIdentityServerUri;
                conf.BackchannelHttpHandler = new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                };

                conf.TokenValidationParameters = new TokenValidationParameters
                {
                    ClockSkew = TimeSpan.FromSeconds(40),
                    ValidateAudience = false,
                };
            });

        services.AddAuthorization(conf => { conf.AddPolicy("Admin", policy => policy.RequireClaim(ClaimTypes.Role, "admin")); });

        return services;
    }

    public static IServiceCollection AddSwaggerService(this IServiceCollection services, IConfiguration configuration)
    {
        string authorizationIdentityServerUri = configuration.GetValue<string>("IdentityUrlName") + "connect/token";

        services.AddSwaggerGen(conf =>
        {
            conf.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "IdentityServerAuthorization.API.xml"));
            conf.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme()
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows()
                {
                    Password = new OpenApiOAuthFlow()
                    {
                        TokenUrl = new Uri(authorizationIdentityServerUri),
                        Scopes = new Dictionary<string, string>()
                        {
                            {"vhub", string.Empty}
                        }
                    },
                },
            });

            conf.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme()
                    {
                        Reference = new OpenApiReference()
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = JwtBearerDefaults.AuthenticationScheme,
                        In = ParameterLocation.Header,
                    },
                    new List<string>()
                }
            });
        });

        return services;
    }
}
