using CustomCADs.Shared.Abstractions.Cache;
using Microsoft.Extensions.Caching.Memory;

namespace CustomCADs.Shared.Infrastructure.Cache;

public class MemoryCacheService(IMemoryCache cache)
	: MemoryCacheService<string>(cache), ICacheService;

public class MemoryCacheService<TKey>(IMemoryCache cache) : ICacheService<TKey> where TKey : class
{
	public async Task<TItem?> GetOrCreateAsync<TItem>(TKey key, Func<Task<TItem>> factory) where TItem : class?
		=> await cache.GetOrCreateAsync(key, (_) => factory()).ConfigureAwait(false);

	public async Task<TItem?> GetAsync<TItem>(TKey key) where TItem : class?
		=> await Task.Run(() =>
			cache.TryGetValue(key, out TItem? result) ? result : null
		).ConfigureAwait(false);

	public async Task SetAsync<TItem>(TKey key, TItem item)
		=> await Task.Run(() =>
			cache.Set(key, item)
		).ConfigureAwait(false);

	public async Task RemoveAsync(TKey key)
		=> await Task.Run(() =>
			cache.Remove(key)
		).ConfigureAwait(false);
}
