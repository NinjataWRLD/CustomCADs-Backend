using CustomCADs.Delivery.Application.Contracts;
using CustomCADs.Delivery.Infrastructure;
using Microsoft.Extensions.Options;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
	public static void AddDeliveryService(this IServiceCollection services)
	{
		services.AddSpeedyService((provider) =>
		{
			using IServiceScope scope = provider.CreateScope();
			DeliverySettings settings = scope.ServiceProvider
				.GetRequiredService<IOptions<DeliverySettings>>()
				.Value;

			return new(
				Account: settings.Account,
				Pickup: settings.Pickup,
				Contact: settings.Contact
			);
		});
		services.AddScoped<IDeliveryService, SpeedyDeliveryService>();
	}
}
