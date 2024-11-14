using CustomCADs.Shared.Speedy.ShipmentService;
using Refit;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    private const string BASE_URL = "https://api.speedy.bg/v1";

    public static IServiceCollection AddShipmentService(this IServiceCollection services)
    {
        services
            .AddRefitClient<IShipmentService>()
            .ConfigureHttpClient(c => c.BaseAddress = new($"{BASE_URL}/shipment"));

        return services;
    }
}
