using CustomCADs.Identity.Application.Common.Dtos;
using CustomCADs.Identity.Application.Common.Services;
using Microsoft.Extensions.Configuration;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddIdentityApplication(this IServiceCollection services, IConfiguration config)
        => services
            .AddServices(config);

    private static IServiceCollection AddServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<IUserService, AppUserService>();

        services.Configure<JwtSettings>(config.GetSection("JwtOptions"));
        services.AddScoped<ITokenService, AppTokenService>();

        return services;
    }
}
