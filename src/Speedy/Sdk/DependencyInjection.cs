using CustomCADs.Speedy.Core.Services.Calculation;
using CustomCADs.Speedy.Core.Services.Client;
using CustomCADs.Speedy.Core.Services.Location;
using CustomCADs.Speedy.Core.Services.Location.Block;
using CustomCADs.Speedy.Core.Services.Location.Complex;
using CustomCADs.Speedy.Core.Services.Location.Country;
using CustomCADs.Speedy.Core.Services.Location.Office;
using CustomCADs.Speedy.Core.Services.Location.Poi;
using CustomCADs.Speedy.Core.Services.Location.PostCode;
using CustomCADs.Speedy.Core.Services.Location.Site;
using CustomCADs.Speedy.Core.Services.Location.State;
using CustomCADs.Speedy.Core.Services.Location.Street;
using CustomCADs.Speedy.Core.Services.Payment;
using CustomCADs.Speedy.Core.Services.Pickup;
using CustomCADs.Speedy.Core.Services.Print;
using CustomCADs.Speedy.Core.Services.Services;
using CustomCADs.Speedy.Core.Services.Shipment;
using CustomCADs.Speedy.Core.Services.Track;
using CustomCADs.Speedy.Core.Services.Validation;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
	public static IServiceCollection AddSpeedyShipment(this IServiceCollection services)
	{
		services.AddShipmentClient();
		services.AddScoped<ShipmentService>();

		return services;
	}

	public static IServiceCollection AddSpeedyPrint(this IServiceCollection services)
	{
		services.AddPrintClient();
		services.AddScoped<PrintService>();

		return services;
	}

	public static IServiceCollection AddSpeedyTrack(this IServiceCollection services)
	{
		services.AddTrackClient();
		services.AddScoped<TrackService>();

		return services;
	}

	public static IServiceCollection AddSpeedyPickup(this IServiceCollection services)
	{
		services.AddPickupClient();
		services.AddScoped<PickupService>();

		return services;
	}

	public static IServiceCollection AddSpeedyLocation(this IServiceCollection services)
	{
		services.AddLocationClient();
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

	public static IServiceCollection AddSpeedyCalculation(this IServiceCollection services)
	{
		services.AddCalculationClient();
		services.AddScoped<CalculationService>();

		return services;
	}

	public static IServiceCollection AddSpeedyClient(this IServiceCollection services)
	{
		services.AddClientClient();
		services.AddScoped<ClientService>();

		return services;
	}

	public static IServiceCollection AddSpeedyValidation(this IServiceCollection services)
	{
		services.AddValidationClient();
		services.AddScoped<ValidationService>();

		return services;
	}

	public static IServiceCollection AddSpeedyServices(this IServiceCollection services)
	{
		services.AddServicesClient();
		services.AddScoped<ServicesService>();

		return services;
	}

	public static IServiceCollection AddSpeedyPayment(this IServiceCollection services)
	{
		services.AddPaymentClient();
		services.AddScoped<PaymentService>();

		return services;
	}
}
