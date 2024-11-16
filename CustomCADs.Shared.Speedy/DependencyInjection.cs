using CustomCADs.Shared.Speedy.Services.LocationService;
using CustomCADs.Shared.Speedy.Services.PickupService;
using CustomCADs.Shared.Speedy.Services.PrintService;
using CustomCADs.Shared.Speedy.Services.ShipmentService;
using CustomCADs.Shared.Speedy.Services.TrackService;
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
    
    public static IServiceCollection AddPrintService(this IServiceCollection services)
    {
        services
            .AddRefitClient<IPrintService>()
            .ConfigureHttpClient(c => c.BaseAddress = new($"{BASE_URL}/print"));

        return services;
    }

    public static IServiceCollection AddTrackService(this IServiceCollection services)
    {
        services
            .AddRefitClient<ITrackService>()
            .ConfigureHttpClient(c => c.BaseAddress = new($"{BASE_URL}/track"));

        return services;
    }

    public static IServiceCollection AddPickupService(this IServiceCollection services)
    {
        services
            .AddRefitClient<IPickupService>()
            .ConfigureHttpClient(c => c.BaseAddress = new($"{BASE_URL}/pickup"));

        return services;
    }

    public static IServiceCollection AddLocationService(this IServiceCollection services)
    {
        services
            .AddRefitClient<ILocationService>()
            .ConfigureHttpClient(c => c.BaseAddress = new($"{BASE_URL}/location"));

        return services;
    }
}
