using CustomCADs.Delivery.Application.Shipments.Caching;
using CustomCADs.Shared.Application.Abstractions.Cache;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

public static partial class DependencyInjection
{
	public static IServiceCollection AddShipmentCaching(this IServiceCollection services)
		=> services.AddScoped<BaseCachingService<ShipmentId, Shipment>, ShipmentCachingService>();
}
