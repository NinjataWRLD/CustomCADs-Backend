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
            .AddCatalogContext(config)
            .AddCatalogReads()
            .AddCatalogWrites()
            .AddCatalogUOW();

    private static IServiceCollection AddCatalogContext(this IServiceCollection services, IConfiguration config)
    {
        string connectionString = config.GetConnectionString("CatalogConnection")
                ?? throw new KeyNotFoundException("Could not find connection string 'CatalogConnection'.");
        services.AddDbContext<CatalogContext>(options => options.UseSqlServer(connectionString));

        return services;
    }

    private static IServiceCollection AddCatalogReads(this IServiceCollection services)
    {
        services.AddScoped<IProductReads, ProductReads>();

        return services;
    }

    private static IServiceCollection AddCatalogWrites(this IServiceCollection services)
    {
        services.AddScoped(typeof(IWrites<>), typeof(Writes<>));

        return services;
    }

    private static IServiceCollection AddCatalogUOW(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
