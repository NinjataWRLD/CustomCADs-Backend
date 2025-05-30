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
using CustomCADs.Shared.Speedy.Services.Calculation;
using CustomCADs.Shared.Speedy.Services.Client;
using CustomCADs.Shared.Speedy.Services.Location;
using CustomCADs.Shared.Speedy.Services.Location.Block;
using CustomCADs.Shared.Speedy.Services.Location.Complex;
using CustomCADs.Shared.Speedy.Services.Location.Country;
using CustomCADs.Shared.Speedy.Services.Location.Office;
using CustomCADs.Shared.Speedy.Services.Location.Poi;
using CustomCADs.Shared.Speedy.Services.Location.PostCode;
using CustomCADs.Shared.Speedy.Services.Location.Site;
using CustomCADs.Shared.Speedy.Services.Location.State;
using CustomCADs.Shared.Speedy.Services.Location.Street;
using CustomCADs.Shared.Speedy.Services.Payment;
using CustomCADs.Shared.Speedy.Services.Pickup;
using CustomCADs.Shared.Speedy.Services.Print;
using CustomCADs.Shared.Speedy.Services.Services;
using CustomCADs.Shared.Speedy.Services.Shipment;
using CustomCADs.Shared.Speedy.Services.Track;
using CustomCADs.Shared.Speedy.Services.Validation;
using Refit;
using System.Text.Json;
using System.Text.Json.Serialization;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
	private const string BASE_URL = "https://api.speedy.bg/v1";

	private static RefitSettings Settings
	{
		get
		{
			JsonSerializerOptions options = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
			options.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.SnakeCaseUpper));
			var serializer = new SystemTextJsonContentSerializer(options);
			return new(serializer, null, null);
		}
	}

	public static IServiceCollection AddDeliveryShipmentService(this IServiceCollection services)
	{
		services
			.AddRefitClient<IShipmentEndpoints>(Settings)
			.ConfigureHttpClient(c => c.BaseAddress = new($"{BASE_URL}/shipment"));
		services.AddScoped<ShipmentService>();

		return services;
	}

	public static IServiceCollection AddDeliveryPrintService(this IServiceCollection services)
	{
		services
			.AddRefitClient<IPrintEndpoints>(Settings)
			.ConfigureHttpClient(c => c.BaseAddress = new($"{BASE_URL}/print"));
		services.AddScoped<PrintService>();

		return services;
	}

	public static IServiceCollection AddDeliveryTrackService(this IServiceCollection services)
	{
		services
			.AddRefitClient<ITrackEndpoints>(Settings)
			.ConfigureHttpClient(c => c.BaseAddress = new($"{BASE_URL}/track"));
		services.AddScoped<TrackService>();

		return services;
	}

	public static IServiceCollection AddDeliveryPickupService(this IServiceCollection services)
	{
		services
			.AddRefitClient<IPickupEndpoints>(Settings)
			.ConfigureHttpClient(c => c.BaseAddress = new($"{BASE_URL}/pickup"));
		services.AddScoped<PickupService>();

		return services;
	}

	public static IServiceCollection AddDeliveryLocationService(this IServiceCollection services)
	{
		services
			.AddRefitClient<ILocationEndpoints>(Settings)
			.ConfigureHttpClient(c => c.BaseAddress = new($"{BASE_URL}/location"));
		services.AddScoped<LocationService>();

		services.AddScoped<BlockService>();
		services.AddScoped<ComplexService>();
		services.AddScoped<CountryService>();
		services.AddScoped<OfficeService>();
		services.AddScoped<PointOfInterestService>();
		services.AddScoped<PostCodeService>();
		services.AddScoped<SiteService>();
		services.AddScoped<StateService>();
		services.AddScoped<StreetService>();

		return services;
	}

	public static IServiceCollection AddDeliveryCalculationService(this IServiceCollection services)
	{
		services
			.AddRefitClient<ICalculationEndpoints>(Settings)
			.ConfigureHttpClient(c => c.BaseAddress = new($"{BASE_URL}/calculate"));
		services.AddScoped<CalculationService>();

		return services;
	}

	public static IServiceCollection AddDeliveryClientService(this IServiceCollection services)
	{
		services
			.AddRefitClient<IClientEndpoints>(Settings)
			.ConfigureHttpClient(c => c.BaseAddress = new($"{BASE_URL}/client"));
		services.AddScoped<ClientService>();

		return services;
	}

	public static IServiceCollection AddDeliveryValidationService(this IServiceCollection services)
	{
		services
			.AddRefitClient<IValidationEndpoints>(Settings)
			.ConfigureHttpClient(c => c.BaseAddress = new($"{BASE_URL}/validation"));
		services.AddScoped<ValidationService>();

		return services;
	}

	public static IServiceCollection AddDeliveryServicesService(this IServiceCollection services)
	{
		services
			.AddRefitClient<IServicesEndpoints>(Settings)
			.ConfigureHttpClient(c => c.BaseAddress = new($"{BASE_URL}/services"));
		services.AddScoped<ServicesService>();

		return services;
	}

	public static IServiceCollection AddDeliveryPaymentService(this IServiceCollection services)
	{
		services
			.AddRefitClient<IPaymentEndpoints>(Settings)
			.ConfigureHttpClient(c => c.BaseAddress = new($"{BASE_URL}/payments"));
		services.AddScoped<PaymentService>();

		return services;
	}
}
