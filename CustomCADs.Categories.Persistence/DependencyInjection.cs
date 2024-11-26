using CustomCADs.Categories.Domain.Categories.Reads;
using CustomCADs.Categories.Domain.Common;
using CustomCADs.Categories.Persistence;
using CustomCADs.Categories.Persistence.Categories.Reads;
using CustomCADs.Categories.Persistence.Common;
using Microsoft.Extensions.Configuration;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddCategoriesPersistence(this IServiceCollection services, IConfiguration config)
        => services
            .AddCategoriesContext(config)
            .AddCategoriesReads()
            .AddCategoriesWrites()
            .AddCategoriesUnitOfWork();

    private static IServiceCollection AddCategoriesContext(this IServiceCollection services, IConfiguration config)
    {
        string connectionString = config.GetConnectionString("CategoriesConnection")
                ?? throw new KeyNotFoundException("Could not find connection string 'CategoriesConnection'.");
        
        services.AddDbContext<CategoriesContext>(options => 
            options.UseSqlServer(connectionString, opt =>
                opt.MigrationsHistoryTable("__EFMigrationsHistory", "Categories")
            )
        );

        return services;
    }

    private static IServiceCollection AddCategoriesReads(this IServiceCollection services)
    {
        services.AddScoped<ICategoryReads, CategoryReads>();

        return services;
    }

    private static IServiceCollection AddCategoriesWrites(this IServiceCollection services)
    {
        services.AddScoped(typeof(IWrites<>), typeof(Writes<>));

        return services;
    }

    private static IServiceCollection AddCategoriesUnitOfWork(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
