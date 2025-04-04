using CustomCADs.Identity.Domain.Repositories;
using CustomCADs.Identity.Infrastructure;
using CustomCADs.Identity.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static async Task<IServiceProvider> UpdateIdentityContextAsync(this IServiceProvider provider)
    {
        IdentityContext context = provider.GetRequiredService<IdentityContext>();
        await context.Database.MigrateAsync().ConfigureAwait(false);

        return provider;
    }

    public static IServiceCollection AddIdentityPersistence(this IServiceCollection services, IConfiguration config)
        => services
            .AddContext(config)
            .AddRepositories();


    private static IServiceCollection AddContext(this IServiceCollection services, IConfiguration config)
    {
        string connectionString = config.GetConnectionString("ApplicationConnection")
            ?? throw new KeyNotFoundException("Could not find connection string 'ApplicationConnection'.");

        services.AddDbContext<IdentityContext>(options =>
            options.UseNpgsql(connectionString, opt =>
                opt.MigrationsHistoryTable("__EFMigrationsHistory", "Identity")
            )
        );

        return services;
    }


    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, AppUserRepository>();
        services.AddScoped<IRoleRepository, AppRoleRepoistory>();

        return services;
    }
}
