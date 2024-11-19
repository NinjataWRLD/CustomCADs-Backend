using CustomCADs.Orders.Domain.Common;
using CustomCADs.Orders.Domain.Orders.Reads;
using CustomCADs.Orders.Persistence;
using CustomCADs.Orders.Persistence.Common;
using CustomCADs.Orders.Persistence.Orders.Reads;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddOrdersPersistence(this IServiceCollection services, IConfiguration config)
        => services
            .AddOrdersContext(config)
            .AddReads()
            .AddWrites()
            .AddUOW();

    private static IServiceCollection AddOrdersContext(this IServiceCollection services, IConfiguration config)
    {
        string connectionString = config.GetConnectionString("OrdersConnection")
            ?? throw new KeyNotFoundException("Could not find connection string 'OrdersConnection'.");
        services.AddDbContext<OrdersContext>(opt => opt.UseSqlServer(connectionString));

        return services;
    }

    public static IServiceCollection AddReads(this IServiceCollection services)
    {
        services.AddScoped<IOrderReads, OrderReads>();

        return services;
    }

    public static IServiceCollection AddWrites(this IServiceCollection services)
    {
        services.AddScoped(typeof(IWrites<>), typeof(Writes<>));

        return services;
    }

    public static IServiceCollection AddUOW(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
