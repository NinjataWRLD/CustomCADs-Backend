using CustomCADs.Orders.Endpoints.Helpers;
using Microsoft.Extensions.Configuration;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddOrders(this IServiceCollection services, IConfiguration config)
        => services
            .AddExceptionHandler<GlobalExceptionHandler>()
            .AddOrdersPersistence(config);
}
