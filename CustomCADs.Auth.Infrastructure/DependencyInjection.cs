using CustomCADs.Auth.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddAuthInfrastructure(this IServiceCollection services, IConfiguration config)    
        => services
            .AddAuthContext(config);
    

    private static IServiceCollection AddAuthContext(this IServiceCollection services, IConfiguration config)
    {
        string connectionString = config.GetConnectionString("AuthConnection")
                ?? throw new KeyNotFoundException("Could not find connection string 'AuthConnection'.");
        services.AddDbContext<AuthContext>(options => options.UseSqlServer(connectionString));
        
        return services;
    }
}
