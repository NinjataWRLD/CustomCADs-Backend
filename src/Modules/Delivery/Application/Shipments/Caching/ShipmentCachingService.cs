using CustomCADs.Shared.Domain.Querying;

namespace CustomCADs.Delivery.Application.Shipments.Caching;

public class ShipmentCachingService(ICacheService service) : BaseCachingService<ShipmentId, Shipment>
{
	private const string BaseKey = "shipments";
	protected override string GetKey() => BaseKey;
	protected override string GetKey(ShipmentId id) => $"{BaseKey}:{id}";

	public async override Task<Result<Shipment>> GetOrCreateAsync(Func<Task<Result<Shipment>>> factory)
		=> await service.GetOrCreateAsync(
			key: GetKey(),
			factory: factory,
			expiration: new(Absolute: TimeSpan.FromDays(7), Sliding: null)
		).ConfigureAwait(false)
		?? throw CustomCachingException<Shipment>.ByKey(GetKey());

	public async override Task<ICollection<Shipment>> GetOrCreateAsync(Func<Task<ICollection<Shipment>>> factory)
		=> await service.GetOrCreateAsync(
			key: GetKey(),
			factory: factory,
			expiration: new(Absolute: TimeSpan.FromDays(7), Sliding: null)
		).ConfigureAwait(false)
		?? throw CustomCachingException<Shipment>.ByKey(GetKey());

	public async override Task<Shipment> GetOrCreateAsync(ShipmentId id, Func<Task<Shipment>> factory)
		=> await service.GetOrCreateAsync(
			key: GetKey(id),
			factory: factory,
			expiration: new(Absolute: TimeSpan.FromDays(7), Sliding: null)
		).ConfigureAwait(false)
		?? throw CustomCachingException<Shipment>.ByKey(GetKey(id));

	public async override Task UpdateAsync(ShipmentId id, Shipment shipment)
	{
		await service.RemoveAsync(GetKey()).ConfigureAwait(false);
		await service.SetAsync(GetKey(id), shipment).ConfigureAwait(false);
	}

	public async override Task ClearAsync(ShipmentId id)
	{
		await service.RemoveAsync(GetKey()).ConfigureAwait(false);
		await service.RemoveAsync(GetKey(id)).ConfigureAwait(false);
	}
}
