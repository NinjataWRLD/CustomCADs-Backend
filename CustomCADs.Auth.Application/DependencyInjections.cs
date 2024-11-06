using CustomCADs.Auth.Application.Dtos;
using CustomCADs.Auth.Application.Services;
using Microsoft.Extensions.Configuration;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjections
{
    public static IServiceCollection AddAuthApplication(this IServiceCollection services, IConfiguration config)
        => services
            .AddIdentityServices()
            .AddTokenService(config);


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
