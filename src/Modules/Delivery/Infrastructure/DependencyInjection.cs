using CustomCADs.Delivery.Application.Contracts;
using CustomCADs.Delivery.Infrastructure;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
	public static void AddDeliveryService(this IServiceCollection services)
	{
		services.AddSpeedyService();
		services.AddScoped<IDeliveryService, SpeedyDeliveryService>();
	}
}
