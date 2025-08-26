using CustomCADs.Shared.Domain.Querying;

namespace CustomCADs.Files.Application.Cads.Caching;

public class CadCachingService(ICacheService service) : BaseCachingService<CadId, Cad>
{
	private const string BaseKey = "cads";
	protected override string GetKey() => BaseKey;
	protected override string GetKey(CadId id) => $"{BaseKey}:{id}";

	public async override Task<Result<Cad>> GetOrCreateAsync(Func<Task<Result<Cad>>> factory)
		=> await service.GetOrCreateAsync(
			key: GetKey(),
			factory: factory,
			expiration: new(Absolute: TimeSpan.FromDays(7), Sliding: null)
		).ConfigureAwait(false)
		?? throw CustomCachingException<Cad>.ByKey(GetKey());

	public async override Task<ICollection<Cad>> GetOrCreateAsync(Func<Task<ICollection<Cad>>> factory)
		=> await service.GetOrCreateAsync(
			key: GetKey(),
			factory: factory,
			expiration: new(Absolute: TimeSpan.FromDays(7), Sliding: null)
		).ConfigureAwait(false)
		?? throw CustomCachingException<Cad>.ByKey(GetKey());

	public async override Task<Cad> GetOrCreateAsync(CadId id, Func<Task<Cad>> factory)
		=> await service.GetOrCreateAsync(
			key: GetKey(id),
			factory: factory,
			expiration: new(Absolute: TimeSpan.FromDays(7), Sliding: null)
		).ConfigureAwait(false)
		?? throw CustomCachingException<Cad>.ByKey(GetKey(id));

	public async override Task UpdateAsync(CadId id, Cad shipment)
	{
		await service.RemoveAsync(GetKey()).ConfigureAwait(false);
		await service.SetAsync(GetKey(id), shipment).ConfigureAwait(false);
	}

	public async override Task ClearAsync(CadId id)
	{
		await service.RemoveAsync(GetKey()).ConfigureAwait(false);
		await service.RemoveAsync(GetKey(id)).ConfigureAwait(false);
	}
}
