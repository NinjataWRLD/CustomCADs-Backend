namespace CustomCADs.Catalog.Application.Categories.Caching;

public class CategoryCachingService(ICacheService service) : BaseCachingService<CategoryId, Category>
{
	private const string BaseKey = "categories";
	protected override string GetKey() => BaseKey;
	protected override string GetKey(CategoryId id) => $"{BaseKey}:${id}";

	public override async Task<ICollection<Category>> GetOrCreateAsync(Func<Task<ICollection<Category>>> factory)
		=> await service.GetOrCreateAsync(GetKey(), factory).ConfigureAwait(false)
			?? throw CustomCachingException<Category>.ByKey(GetKey());

	public override async Task<Category> GetOrCreateAsync(CategoryId id, Func<Task<Category>> factory)
		=> await service.GetOrCreateAsync(GetKey(id), factory).ConfigureAwait(false)
			?? throw CustomCachingException<Category>.ByKey(GetKey(id));

	public override async Task UpdateAsync(CategoryId id, Category Category)
	{
		await service.RemoveAsync(GetKey()).ConfigureAwait(false);
		await service.SetAsync(GetKey(id), Category).ConfigureAwait(false);
	}

	public override async Task ClearAsync(CategoryId id)
	{
		await service.RemoveAsync(GetKey()).ConfigureAwait(false);
		await service.RemoveAsync(GetKey(id)).ConfigureAwait(false);
	}
}
