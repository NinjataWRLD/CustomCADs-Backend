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
    public static IServiceCollection AddInventoryPersistence(this IServiceCollection services, IConfiguration config)
        => services
            .AddInventoryContext(config)
            .AddInventoryReads()
            .AddInventoryWrites()
            .AddInventoryUnitOfWork();

    private static IServiceCollection AddInventoryContext(this IServiceCollection services, IConfiguration config)
    {
        string connectionString = config.GetConnectionString("InventoryConnection")
                ?? throw new KeyNotFoundException("Could not find connection string 'InventoryConnection'.");

        services.AddDbContext<InventoryContext>(options =>
            options.UseSqlServer(connectionString, opt =>
                opt.MigrationsHistoryTable("__EFMigrationsHistory", "Inventory")
            )
        );

        return services;
    }

    private static IServiceCollection AddInventoryReads(this IServiceCollection services)
    {
        services.AddScoped<IProductReads, ProductReads>();

        return services;
    }

    private static IServiceCollection AddInventoryWrites(this IServiceCollection services)
    {
        services.AddScoped(typeof(IWrites<>), typeof(Writes<>));

        return services;
    }

    private static IServiceCollection AddInventoryUnitOfWork(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
