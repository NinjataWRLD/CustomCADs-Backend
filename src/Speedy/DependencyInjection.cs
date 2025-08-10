using CustomCADs.Speedy.API.Endpoints.CalculationEndpoints;
using CustomCADs.Speedy.API.Endpoints.ClientEndpoints;
using CustomCADs.Speedy.API.Endpoints.LocationEndpoints;
using CustomCADs.Speedy.API.Endpoints.PaymentEndpoints;
using CustomCADs.Speedy.API.Endpoints.PickupEndpoints;
using CustomCADs.Speedy.API.Endpoints.PrintEndpoints;
using CustomCADs.Speedy.API.Endpoints.ServicesEndpoints;
using CustomCADs.Speedy.API.Endpoints.ShipmentEndpoints;
using CustomCADs.Speedy.API.Endpoints.TrackEndpoints;
using CustomCADs.Speedy.API.Endpoints.ValidationEndpoints;
using CustomCADs.Speedy.Services.Calculation;
using CustomCADs.Speedy.Services.Client;
using CustomCADs.Speedy.Services.Location;
using CustomCADs.Speedy.Services.Location.Block;
using CustomCADs.Speedy.Services.Location.Complex;
using CustomCADs.Speedy.Services.Location.Country;
using CustomCADs.Speedy.Services.Location.Office;
using CustomCADs.Speedy.Services.Location.Poi;
using CustomCADs.Speedy.Services.Location.PostCode;
using CustomCADs.Speedy.Services.Location.Site;
using CustomCADs.Speedy.Services.Location.State;
using CustomCADs.Speedy.Services.Location.Street;
using CustomCADs.Speedy.Services.Payment;
using CustomCADs.Speedy.Services.Pickup;
using CustomCADs.Speedy.Services.Print;
using CustomCADs.Speedy.Services.Services;
using CustomCADs.Speedy.Services.Shipment;
using CustomCADs.Speedy.Services.Track;
using CustomCADs.Speedy.Services.Validation;
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

	public static IServiceCollection AddSpeedyShipmentService(this IServiceCollection services)
	{
		services
			.AddRefitClient<IShipmentEndpoints>(Settings)
			.ConfigureHttpClient(c => c.BaseAddress = new($"{BASE_URL}/shipment"));
		services.AddScoped<ShipmentService>();

		return services;
	}

	public static IServiceCollection AddSpeedyPrintService(this IServiceCollection services)
	{
		services
			.AddRefitClient<IPrintEndpoints>(Settings)
			.ConfigureHttpClient(c => c.BaseAddress = new($"{BASE_URL}/print"));
		services.AddScoped<PrintService>();

		return services;
	}

	public static IServiceCollection AddSpeedyTrackService(this IServiceCollection services)
	{
		services
			.AddRefitClient<ITrackEndpoints>(Settings)
			.ConfigureHttpClient(c => c.BaseAddress = new($"{BASE_URL}/track"));
		services.AddScoped<TrackService>();

		return services;
	}

	public static IServiceCollection AddSpeedyPickupService(this IServiceCollection services)
	{
		services
			.AddRefitClient<IPickupEndpoints>(Settings)
			.ConfigureHttpClient(c => c.BaseAddress = new($"{BASE_URL}/pickup"));
		services.AddScoped<PickupService>();

		return services;
	}

	public static IServiceCollection AddSpeedyLocationService(this IServiceCollection services)
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

	public static IServiceCollection AddSpeedyCalculationService(this IServiceCollection services)
	{
		services
			.AddRefitClient<ICalculationEndpoints>(Settings)
			.ConfigureHttpClient(c => c.BaseAddress = new($"{BASE_URL}/calculate"));
		services.AddScoped<CalculationService>();

		return services;
	}

	public static IServiceCollection AddSpeedyClientService(this IServiceCollection services)
	{
		services
			.AddRefitClient<IClientEndpoints>(Settings)
			.ConfigureHttpClient(c => c.BaseAddress = new($"{BASE_URL}/client"));
		services.AddScoped<ClientService>();

		return services;
	}

	public static IServiceCollection AddSpeedyValidationService(this IServiceCollection services)
	{
		services
			.AddRefitClient<IValidationEndpoints>(Settings)
			.ConfigureHttpClient(c => c.BaseAddress = new($"{BASE_URL}/validation"));
		services.AddScoped<ValidationService>();

		return services;
	}

	public static IServiceCollection AddSpeedyServicesService(this IServiceCollection services)
	{
		services
			.AddRefitClient<IServicesEndpoints>(Settings)
			.ConfigureHttpClient(c => c.BaseAddress = new($"{BASE_URL}/services"));
		services.AddScoped<ServicesService>();

		return services;
	}

	public static IServiceCollection AddSpeedyPaymentService(this IServiceCollection services)
	{
		services
			.AddRefitClient<IPaymentEndpoints>(Settings)
			.ConfigureHttpClient(c => c.BaseAddress = new($"{BASE_URL}/payments"));
		services.AddScoped<PaymentService>();

		return services;
	}
}
