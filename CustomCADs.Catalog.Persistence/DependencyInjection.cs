using CustomCADs.Catalog.Domain.Categories.Reads;
using CustomCADs.Catalog.Domain.Shared;
using CustomCADs.Catalog.Persistence.Repositories;
using CustomCADs.Catalog.Persistence.Repositories.Reads;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CustomCADs.Catalog.Persistence;

public static class DependencyInjection
{
    public static void AddApplicationContext(this IServiceCollection services, IConfiguration config)
    {
        string connectionString = config.GetConnectionString("RealConnection")
                ?? throw new KeyNotFoundException("Could not find connection string 'RealConnection'.");
        services.AddDbContext<CatalogContext>(options => options.UseSqlServer(connectionString));
    }

    public static void AddReads(this IServiceCollection services)
    {
        services.AddScoped<ICategoryReads, CategoryReads>();
    }

    public static void AddWrites(this IServiceCollection services)
    {
        services.AddScoped(typeof(IWrites<>), typeof(Writes<>));
    }
}
