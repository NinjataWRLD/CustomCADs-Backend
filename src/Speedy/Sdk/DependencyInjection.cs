using CustomCADs.Speedy.Core.Contracts.Calculation;
using CustomCADs.Speedy.Core.Contracts.Client;
using CustomCADs.Speedy.Core.Contracts.Location;
using CustomCADs.Speedy.Core.Contracts.Payment;
using CustomCADs.Speedy.Core.Contracts.Pickup;
using CustomCADs.Speedy.Core.Contracts.Print;
using CustomCADs.Speedy.Core.Contracts.Services;
using CustomCADs.Speedy.Core.Contracts.Shipment;
using CustomCADs.Speedy.Core.Contracts.Track;
using CustomCADs.Speedy.Core.Contracts.Validation;
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

using CustomCADs.Speedy.Sdk;

public static class DependencyInjection
{
	public static IServiceCollection AddSpeedyService(this IServiceCollection services, Func<IServiceProvider, SpeedyOptions> optionsFunc)
	{
		return services
				.AddSpeedyShipment()
				.AddSpeedyPrint()
				.AddSpeedyTrack()
				.AddSpeedyPickup()
				.AddSpeedyLocation()
				.AddSpeedyCalculation()
				.AddSpeedyClient()
				.AddSpeedyValidation()
				.AddSpeedyServices()
				.AddSpeedyPayment()
				.AddScoped(optionsFunc)
				.AddScoped<ISpeedyService, SpeedyService>();
	}

	private static IServiceCollection AddSpeedyShipment(this IServiceCollection services)
	{
		services.AddShipmentClient();
		services.AddScoped<IShipmentService, ShipmentService>();

		return services;
	}

	private static IServiceCollection AddSpeedyPrint(this IServiceCollection services)
	{
		services.AddPrintClient();
		services.AddScoped<IPrintService, PrintService>();

		return services;
	}

	private static IServiceCollection AddSpeedyTrack(this IServiceCollection services)
	{
		services.AddTrackClient();
		services.AddScoped<ITrackService, TrackService>();

		return services;
	}

	private static IServiceCollection AddSpeedyPickup(this IServiceCollection services)
	{
		services.AddPickupClient();
		services.AddScoped<IPickupService, PickupService>();

		return services;
	}

	private static IServiceCollection AddSpeedyLocation(this IServiceCollection services)
	{
		services.AddLocationClient();
		services.AddScoped<ILocationService, LocationService>();

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

	private static IServiceCollection AddSpeedyCalculation(this IServiceCollection services)
	{
		services.AddCalculationClient();
		services.AddScoped<ICalculationService, CalculationService>();

		return services;
	}

	private static IServiceCollection AddSpeedyClient(this IServiceCollection services)
	{
		services.AddClientClient();
		services.AddScoped<IClientService, ClientService>();

		return services;
	}

	private static IServiceCollection AddSpeedyValidation(this IServiceCollection services)
	{
		services.AddValidationClient();
		services.AddScoped<IValidationService, ValidationService>();

		return services;
	}

	private static IServiceCollection AddSpeedyServices(this IServiceCollection services)
	{
		services.AddServicesClient();
		services.AddScoped<IServicesService, ServicesService>();

		return services;
	}

	private static IServiceCollection AddSpeedyPayment(this IServiceCollection services)
	{
		services.AddPaymentClient();
		services.AddScoped<IPaymentService, PaymentService>();

		return services;
	}
}
