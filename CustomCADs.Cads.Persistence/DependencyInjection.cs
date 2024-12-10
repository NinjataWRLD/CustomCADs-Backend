using CustomCADs.Cads.Domain.Cads.Reads;
using CustomCADs.Cads.Domain.Common;
using CustomCADs.Cads.Persistence;
using CustomCADs.Cads.Persistence.Cads.Reads;
using CustomCADs.Cads.Persistence.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static async Task<IServiceProvider> UpdateCadsContextAsync(this IServiceProvider provider)
    {
        CadsContext context = provider.GetRequiredService<CadsContext>();
        await context.Database.MigrateAsync().ConfigureAwait(false);

        return provider;
    }

    public static IServiceCollection AddCadsPersistence(this IServiceCollection services, IConfiguration config)
        => services
            .AddContext(config)
            .AddReads()
            .AddWrites()
            .AddUnitOfWork();

    public static IServiceCollection AddContext(this IServiceCollection services, IConfiguration config)
    {
        string connectionString = config.GetConnectionString("ApplicationConnection")
            ?? throw new KeyNotFoundException("Could not find connection string 'ApplicationConnection'.");

        services.AddDbContext<CadsContext>(options =>
            options.UseNpgsql(connectionString, opt =>
                opt.MigrationsHistoryTable("__EFMigrationsHistory", "Cads")
            )
        );

        return services;
    }

    public static IServiceCollection AddReads(this IServiceCollection services)
    {
        services.AddScoped<ICadReads, CadReads>();

        return services;
    }

    public static IServiceCollection AddWrites(this IServiceCollection services)
    {
        services.AddScoped(typeof(IWrites<>), typeof(Writes<>));

        return services;
    }

    public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
