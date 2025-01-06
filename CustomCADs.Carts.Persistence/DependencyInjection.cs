using CustomCADs.Carts.Domain.ActiveCarts.Reads;
using CustomCADs.Carts.Domain.Common;
using CustomCADs.Carts.Domain.PurchasedCarts.Reads;
using CustomCADs.Carts.Persistence;
using CustomCADs.Carts.Persistence.ActiveCarts.Reads;
using CustomCADs.Carts.Persistence.Common;
using CustomCADs.Carts.Persistence.PurchasedCarts.Reads;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static async Task<IServiceProvider> UpdateCartsContextAsync(this IServiceProvider provider)
    {
        CartsContext context = provider.GetRequiredService<CartsContext>();
        await context.Database.MigrateAsync().ConfigureAwait(false);

        return provider;
    }

    public static IServiceCollection AddCartsPersistence(this IServiceCollection services, IConfiguration config)
        => services
            .AddContext(config)
            .AddReads()
            .AddWrites()
            .AddUnitOfWork();

    private static IServiceCollection AddContext(this IServiceCollection services, IConfiguration config)
    {
        string connectionString = config.GetConnectionString("ApplicationConnection")
            ?? throw new KeyNotFoundException("Could not find connection string 'ApplicationConnection'.");

        services.AddDbContext<CartsContext>(options =>
            options.UseNpgsql(connectionString, opt
                => opt.MigrationsHistoryTable("__EFMigrationsHistory", "Carts")
            )
        );

        return services;
    }

    public static IServiceCollection AddReads(this IServiceCollection services)
    {
        services.AddScoped<IActiveCartReads, ActiveCartReads>();
        services.AddScoped<IPurchasedCartReads, PurchasedCartReads>();

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
