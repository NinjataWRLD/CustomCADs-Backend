using CustomCADs.Orders.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddOrdersPersistence(this IServiceCollection services, IConfiguration config)
        => services
            .AddOrdersContext(config);

    public static IServiceCollection AddOrdersContext(this IServiceCollection services, IConfiguration config)
    {
        string connectionString = config.GetConnectionString("OrdersConnection")
            ?? throw new KeyNotFoundException("Could not find connection string 'OrdersConnection'.");
        services.AddDbContext<OrdersContext>(opt => opt.UseSqlServer(connectionString));

        return services;
    }
}
