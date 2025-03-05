using CustomCADs.Customizations.Domain.Common;
using CustomCADs.Customizations.Domain.Customizations.Reads;
using CustomCADs.Customizations.Domain.Materials.Reads;
using CustomCADs.Customizations.Persistence;
using CustomCADs.Customizations.Persistence.Common;
using CustomCADs.Customizations.Persistence.Customizations.Reads;
using CustomCADs.Customizations.Persistence.Materials.Reads;
using Microsoft.Extensions.Configuration;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static async Task<IServiceProvider> UpdateCustomizationsContextAsync(this IServiceProvider provider)
    {
        CustomizationsContext context = provider.GetRequiredService<CustomizationsContext>();
        await context.Database.MigrateAsync().ConfigureAwait(false);

        return provider;
    }

    public static IServiceCollection AddCustomizationsPersistence(this IServiceCollection services, IConfiguration config)
        => services
            .AddContext(config)
            .AddReads()
            .AddWrites()
            .AddUnitOfWork();

    private static IServiceCollection AddContext(this IServiceCollection services, IConfiguration config)
    {
        string connectionString = config.GetConnectionString("ApplicationConnection")
            ?? throw new KeyNotFoundException("Could not find connection string 'ApplicationConnection'.");

        services.AddDbContext<CustomizationsContext>(options =>
            options.UseNpgsql(connectionString, opt =>
                opt.MigrationsHistoryTable("__EFMigrationsHistory", "Categories")
            )
        );

        return services;
    }

    private static IServiceCollection AddReads(this IServiceCollection services)
    {
        services.AddScoped<ICustomizationReads, CustomizationReads>();
        services.AddScoped<IMaterialReads, MaterialReads>();

        return services;
    }

    private static IServiceCollection AddWrites(this IServiceCollection services)
    {
        services.AddScoped(typeof(IWrites<>), typeof(Writes<>));

        return services;
    }

    private static IServiceCollection AddUnitOfWork(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
