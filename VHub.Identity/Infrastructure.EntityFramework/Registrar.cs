using Domain.Entities;
using Infrastructure.EntityFramework.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

namespace Infrastructure.EntityFramework;

public static class Registrar
{
    public static void InitializeDb(this IServiceCollection services, IConfiguration configuration)
    {
        try
        {
            var dbConnectionString = GetDbConnectionString(configuration);
            services.AddDbContext<AuthDbContext>(conf =>
                conf.UseNpgsql(dbConnectionString!))
                .AddIdentity<User, Role>(conf =>
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
        catch
        {
            throw;
        }
    }
    /// <summary>
    /// Получить строку подключения к базе данных 
    /// </summary>
    /// <param name="configuration">Конфигурация</param>
    private static string GetDbConnectionString(IConfiguration configuration)
    {
        string sectionName = "AppDataBase";
        var section = configuration.GetSection(sectionName);

        var dbHost = section["Host"];
        var dbDatabase = section["Database"];
        var dbUsername = section["User"];
        var dbPassword = section["Password"];
        var dbPort = section["Port"];

        var connectionStringBuilder = new StringBuilder();
        connectionStringBuilder.Append($"Host={dbHost};");
        connectionStringBuilder.Append($"Port={dbPort};");
        connectionStringBuilder.Append($"Database={dbDatabase};");
        connectionStringBuilder.Append($"User Id={dbUsername};");
        connectionStringBuilder.Append($"Password={dbPassword};");

        return connectionStringBuilder.ToString();
    }
}
