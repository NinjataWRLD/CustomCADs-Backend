using CustomCADs.Catalog.Domain.Categories.Reads;
using CustomCADs.Catalog.Domain.Products.Reads;
using CustomCADs.Catalog.Domain.Shared;
using CustomCADs.Catalog.Persistence;
using CustomCADs.Catalog.Persistence.Repositories;
using CustomCADs.Catalog.Persistence.Repositories.Reads;
using CustomCADs.Shared.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static void AddCatalogContext(this IServiceCollection services, IConfiguration config)
    {
        string connectionString = config.GetConnectionString("CatalogConnection")
                ?? throw new KeyNotFoundException("Could not find connection string 'CatalogConnection'.");
        services.AddDbContext<CatalogContext>(options => options.UseSqlServer(connectionString));
    }

    public static void AddCatalogReads(this IServiceCollection services)
    {
        services.AddScoped<ICategoryReads, CategoryReads>();
        services.AddScoped<IProductReads, ProductReads>();
    }

    public static void AddCatalogWrites(this IServiceCollection services)
    {
        services.AddScoped(typeof(IWrites<>), typeof(Writes<>));
    }

    public static void AddCatalogUOW(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}
