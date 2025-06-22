namespace CustomCADs.Shared.Abstractions.Cache;

/// <summary>
/// 	Use this base class to implement custom Caching Services which inject the ICacheService
/// </summary>
/// <typeparam name="TId">The Id of the resource to cache</typeparam>
/// <typeparam name="TItem">The resource to cache</typeparam>
public abstract class BaseCachingService<TId, TItem>
{
	protected abstract string GetKey();
	protected abstract string GetKey(TId id);

	public abstract Task<ICollection<TItem>> GetOrCreateAsync(Func<Task<ICollection<TItem>>> factory);
	public abstract Task<TItem> GetOrCreateAsync(TId id, Func<Task<TItem>> factory);
	public abstract Task UpdateAsync(TId id, TItem TItem);
	public abstract Task ClearAsync(TId id);
}
