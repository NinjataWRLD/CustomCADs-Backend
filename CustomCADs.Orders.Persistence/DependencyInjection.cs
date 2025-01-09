using CustomCADs.Orders.Domain.Common;
using CustomCADs.Orders.Domain.CompletedOrders.Reads;
using CustomCADs.Orders.Domain.OngoingOrders.Reads;
using CustomCADs.Orders.Persistence;
using CustomCADs.Orders.Persistence.Common;
using CustomCADs.Orders.Persistence.CompletedOrders.Reads;
using CustomCADs.Orders.Persistence.OngoingOrders.Reads;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static async Task<IServiceProvider> UpdateOrdersContextAsync(this IServiceProvider provider)
    {
        OrdersContext context = provider.GetRequiredService<OrdersContext>();
        await context.Database.MigrateAsync().ConfigureAwait(false);

        return provider;
    }

    public static IServiceCollection AddOrdersPersistence(this IServiceCollection services, IConfiguration config)
        => services
            .AddContext(config)
            .AddReads()
            .AddWrites()
            .AddUnitOfWork();

    private static IServiceCollection AddContext(this IServiceCollection services, IConfiguration config)
    {
        string connectionString = config.GetConnectionString("ApplicationConnection")
            ?? throw new KeyNotFoundException("Could not find connection string 'ApplicationConnection'.");

        services.AddDbContext<OrdersContext>(opt =>
            opt.UseNpgsql(connectionString, opt =>
                opt.MigrationsHistoryTable("__EFMigrationsHistory", "Orders")
            )
        );

        return services;
    }

    private static IServiceCollection AddReads(this IServiceCollection services)
    {
        services.AddScoped<IOngoingOrderReads, OngoingOrderReads>();
        services.AddScoped<ICompletedOrderReads, CompletedOrderReads>();

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
