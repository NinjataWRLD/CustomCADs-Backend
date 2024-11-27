using CustomCADs.Auth.Application.Contracts;
using CustomCADs.Auth.Infrastructure;
using CustomCADs.Auth.Infrastructure.Dtos;
using CustomCADs.Auth.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddAuthInfrastructure(this IServiceCollection services, IConfiguration config)
        => services
            .AddAuthContext(config)
            .AddIdentityServices()
            .AddTokenService(config);


    private static IServiceCollection AddAuthContext(this IServiceCollection services, IConfiguration config)
    {
        string connectionString = config.GetConnectionString("AuthConnection")
                ?? throw new KeyNotFoundException("Could not find connection string 'AuthConnection'.");

        services.AddDbContext<AuthContext>(options =>
            options.UseSqlServer(connectionString, opt =>
                opt.MigrationsHistoryTable("__EFMigrationsHistory", "Auth")
            )
        );

        return services;
    }


    private static IServiceCollection AddIdentityServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, AppUserService>();
        services.AddScoped<IRoleService, AppRoleService>();

        return services;
    }

    private static IServiceCollection AddTokenService(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<JwtSettings>(config.GetSection("JwtSettings"));
        services.AddScoped<ITokenService, AppTokenService>();

        return services;
    }
}
