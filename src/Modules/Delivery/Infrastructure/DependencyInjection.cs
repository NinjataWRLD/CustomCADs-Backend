using CustomCADs.Delivery.Application.Contracts;
using CustomCADs.Delivery.Infrastructure;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
	public static void AddDeliveryService(this IServiceCollection services)
	{
		services.AddSpeedyShipmentService();
		services.AddSpeedyPrintService();
		services.AddSpeedyTrackService();
		services.AddSpeedyPickupService();
		services.AddSpeedyLocationService();
		services.AddSpeedyCalculationService();
		services.AddSpeedyClientService();
		services.AddSpeedyValidationService();
		services.AddSpeedyServicesService();
		services.AddSpeedyPaymentService();

		services.AddScoped<IDeliveryService, SpeedyService>();
	}
}
