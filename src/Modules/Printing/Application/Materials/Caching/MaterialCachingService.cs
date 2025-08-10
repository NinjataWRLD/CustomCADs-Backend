namespace CustomCADs.Printing.Application.Materials.Caching;

public class MaterialCachingService(ICacheService service) : BaseCachingService<MaterialId, Material>
{
	private const string BaseKey = "materials";
	protected override string GetKey() => BaseKey;
	protected override string GetKey(MaterialId id) => $"{BaseKey}:${id}";

	public override async Task<ICollection<Material>> GetOrCreateAsync(Func<Task<ICollection<Material>>> factory)
		=> await service.GetOrCreateAsync(GetKey(), factory).ConfigureAwait(false)
			?? throw CustomCachingException<Material>.ByKey(GetKey());

	public override async Task<Material> GetOrCreateAsync(MaterialId id, Func<Task<Material>> factory)
		=> await service.GetOrCreateAsync(GetKey(id), factory).ConfigureAwait(false)
			?? throw CustomCachingException<Material>.ByKey(GetKey(id));

	public override async Task UpdateAsync(MaterialId id, Material material)
	{
		await service.RemoveAsync(GetKey()).ConfigureAwait(false);
		await service.SetAsync(GetKey(id), material).ConfigureAwait(false);
	}

	public override async Task ClearAsync(MaterialId id)
	{
		await service.RemoveAsync(GetKey()).ConfigureAwait(false);
		await service.RemoveAsync(GetKey(id)).ConfigureAwait(false);
	}
}
