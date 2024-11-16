using CustomCADs.Shared.Speedy.Services.CalculationService;
using CustomCADs.Shared.Speedy.Services.ClientService;
using CustomCADs.Shared.Speedy.Services.LocationService;
using CustomCADs.Shared.Speedy.Services.PaymentService;
using CustomCADs.Shared.Speedy.Services.PickupService;
using CustomCADs.Shared.Speedy.Services.PrintService;
using CustomCADs.Shared.Speedy.Services.ServicesService;
using CustomCADs.Shared.Speedy.Services.ShipmentService;
using CustomCADs.Shared.Speedy.Services.TrackService;
using CustomCADs.Shared.Speedy.Services.ValidationService;
using Refit;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    private const string BASE_URL = "https://api.speedy.bg/v1";

    public static IServiceCollection AddDeliveryShipmentService(this IServiceCollection services)
    {
        services
            .AddRefitClient<IShipmentService>()
            .ConfigureHttpClient(c => c.BaseAddress = new($"{BASE_URL}/shipment"));

        return services;
    }
    
    public static IServiceCollection AddDeliveryPrintService(this IServiceCollection services)
    {
        services
            .AddRefitClient<IPrintService>()
            .ConfigureHttpClient(c => c.BaseAddress = new($"{BASE_URL}/print"));

        return services;
    }

    public static IServiceCollection AddDeliveryTrackService(this IServiceCollection services)
    {
        services
            .AddRefitClient<ITrackService>()
            .ConfigureHttpClient(c => c.BaseAddress = new($"{BASE_URL}/track"));

        return services;
    }

    public static IServiceCollection AddDeliveryPickupService(this IServiceCollection services)
    {
        services
            .AddRefitClient<IPickupService>()
            .ConfigureHttpClient(c => c.BaseAddress = new($"{BASE_URL}/pickup"));

        return services;
    }

    public static IServiceCollection AddDeliveryLocationService(this IServiceCollection services)
    {
        services
            .AddRefitClient<ILocationService>()
            .ConfigureHttpClient(c => c.BaseAddress = new($"{BASE_URL}/location"));

        return services;
    }

    public static IServiceCollection AddDeliveryCalculationService(this IServiceCollection services)
    {
        services
            .AddRefitClient<ICalculationService>()
            .ConfigureHttpClient(c => c.BaseAddress = new($"{BASE_URL}/calculate"));

        return services;
    }

    public static IServiceCollection AddDeliveryClientService(this IServiceCollection services)
    {
        services
            .AddRefitClient<IClientService>()
            .ConfigureHttpClient(c => c.BaseAddress = new($"{BASE_URL}/client"));

        return services;
    }

    public static IServiceCollection AddDeliveryValidationService(this IServiceCollection services)
    {
        services
            .AddRefitClient<IValidationService>()
            .ConfigureHttpClient(c => c.BaseAddress = new($"{BASE_URL}/validation"));

        return services;
    }

    public static IServiceCollection AddDeliveryServicesService(this IServiceCollection services)
    {
        services
            .AddRefitClient<IServicesService>()
            .ConfigureHttpClient(c => c.BaseAddress = new($"{BASE_URL}/services"));

        return services;
    }

    public static IServiceCollection AddDeliveryPaymentService(this IServiceCollection services)
    {
        services
            .AddRefitClient<IPaymentService>()
            .ConfigureHttpClient(c => c.BaseAddress = new($"{BASE_URL}/payments"));

        return services;
    }
}
