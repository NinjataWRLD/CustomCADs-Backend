using CustomCADs.Shared.Speedy.API.Endpoints.CalculationEndpoints;
using CustomCADs.Shared.Speedy.API.Endpoints.ClientEndpoints;
using CustomCADs.Shared.Speedy.API.Endpoints.LocationEndpoints;
using CustomCADs.Shared.Speedy.API.Endpoints.PaymentEndpoints;
using CustomCADs.Shared.Speedy.API.Endpoints.PickupEndpoints;
using CustomCADs.Shared.Speedy.API.Endpoints.PrintEndpoints;
using CustomCADs.Shared.Speedy.API.Endpoints.ServicesEndpoints;
using CustomCADs.Shared.Speedy.API.Endpoints.ShipmentEndpoints;
using CustomCADs.Shared.Speedy.API.Endpoints.TrackEndpoints;
using CustomCADs.Shared.Speedy.API.Endpoints.ValidationEndpoints;
using Refit;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    private const string BASE_URL = "https://api.speedy.bg/v1";

    public static IServiceCollection AddDeliveryShipmentService(this IServiceCollection services)
    {
        services
            .AddRefitClient<IShipmentEndpoints>()
            .ConfigureHttpClient(c => c.BaseAddress = new($"{BASE_URL}/shipment"));

        return services;
    }

    public static IServiceCollection AddDeliveryPrintService(this IServiceCollection services)
    {
        services
            .AddRefitClient<IPrintEndpoints>()
            .ConfigureHttpClient(c => c.BaseAddress = new($"{BASE_URL}/print"));

        return services;
    }

    public static IServiceCollection AddDeliveryTrackService(this IServiceCollection services)
    {
        services
            .AddRefitClient<ITrackEndpoints>()
            .ConfigureHttpClient(c => c.BaseAddress = new($"{BASE_URL}/track"));

        return services;
    }

    public static IServiceCollection AddDeliveryPickupService(this IServiceCollection services)
    {
        services
            .AddRefitClient<IPickupEndpoints>()
            .ConfigureHttpClient(c => c.BaseAddress = new($"{BASE_URL}/pickup"));

        return services;
    }

    public static IServiceCollection AddDeliveryLocationService(this IServiceCollection services)
    {
        services
            .AddRefitClient<ILocationEndpoints>()
            .ConfigureHttpClient(c => c.BaseAddress = new($"{BASE_URL}/location"));

        return services;
    }

    public static IServiceCollection AddDeliveryCalculationService(this IServiceCollection services)
    {
        services
            .AddRefitClient<ICalculationEndpoints>()
            .ConfigureHttpClient(c => c.BaseAddress = new($"{BASE_URL}/calculate"));

        return services;
    }

    public static IServiceCollection AddDeliveryClientService(this IServiceCollection services)
    {
        services
            .AddRefitClient<IClientEndpoints>()
            .ConfigureHttpClient(c => c.BaseAddress = new($"{BASE_URL}/client"));

        return services;
    }

    public static IServiceCollection AddDeliveryValidationService(this IServiceCollection services)
    {
        services
            .AddRefitClient<IValidationEndpoints>()
            .ConfigureHttpClient(c => c.BaseAddress = new($"{BASE_URL}/validation"));

        return services;
    }

    public static IServiceCollection AddDeliveryServicesService(this IServiceCollection services)
    {
        services
            .AddRefitClient<IServicesEndpoints>()
            .ConfigureHttpClient(c => c.BaseAddress = new($"{BASE_URL}/services"));

        return services;
    }

    public static IServiceCollection AddDeliveryPaymentService(this IServiceCollection services)
    {
        services
            .AddRefitClient<IPaymentEndpoints>()
            .ConfigureHttpClient(c => c.BaseAddress = new($"{BASE_URL}/payments"));

        return services;
    }
}
