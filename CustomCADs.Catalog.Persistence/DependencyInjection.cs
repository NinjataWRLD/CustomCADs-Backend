using CustomCADs.Catalog.Domain.Common;
using CustomCADs.Catalog.Domain.Products.Reads;
using CustomCADs.Catalog.Persistence;
using CustomCADs.Catalog.Persistence.Common;
using CustomCADs.Catalog.Persistence.Products.Reads;
using Microsoft.Extensions.Configuration;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddCatalogPersistence(this IServiceCollection services, IConfiguration config)
        => services
            .AddContext(config)
            .AddReads()
            .AddWrites()
            .AddUnitOfWork();

    private static IServiceCollection AddContext(this IServiceCollection services, IConfiguration config)
    {
        string connectionString = config.GetConnectionString("CatalogConnection")
                ?? throw new KeyNotFoundException("Could not find connection string 'CatalogConnection'.");

        services.AddDbContext<CatalogContext>(options =>
            options.UseSqlServer(connectionString, opt =>
                opt.MigrationsHistoryTable("__EFMigrationsHistory", "Catalog")
            )
        );

        return services;
    }

    private static IServiceCollection AddReads(this IServiceCollection services)
    {
        services.AddScoped<IProductReads, ProductReads>();

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
