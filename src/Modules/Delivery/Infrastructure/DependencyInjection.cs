using CustomCADs.Delivery.Application.Contracts;
using CustomCADs.Delivery.Infrastructure;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
	public static void AddDeliveryService(this IServiceCollection services)
	{
		services.AddSpeedyShipment();
		services.AddSpeedyPrint();
		services.AddSpeedyTrack();
		services.AddSpeedyPickup();
		services.AddSpeedyLocation();
		services.AddSpeedyCalculation();
		services.AddSpeedyClient();
		services.AddSpeedyValidation();
		services.AddSpeedyServices();
		services.AddSpeedyPayment();

		services.AddScoped<IDeliveryService, SpeedyService>();
	}
}
