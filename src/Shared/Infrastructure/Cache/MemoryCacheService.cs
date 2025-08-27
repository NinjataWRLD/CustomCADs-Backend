using CustomCADs.Shared.Application.Abstractions.Cache;
using Microsoft.Extensions.Caching.Memory;

namespace CustomCADs.Shared.Infrastructure.Cache;

public class MemoryCacheService(IMemoryCache cache)
	: MemoryCacheService<string>(cache), ICacheService;

public class MemoryCacheService<TKey>(IMemoryCache cache) : ICacheService<TKey> where TKey : notnull
{
	public async Task<TItem?> GetOrCreateAsync<TItem>(
		TKey key,
		Func<Task<TItem>> factory,
		Expiration? expiration = null
	) => await cache.GetOrCreateAsync(
			key: key,
			factory: (_) => factory(),
			createOptions: new()
			{
				AbsoluteExpirationRelativeToNow = expiration?.Absolute,
				SlidingExpiration = expiration?.Sliding,
			}
		).ConfigureAwait(false);

	public Task<TItem?> GetAsync<TItem>(TKey key) where TItem : class?
		=> Task.FromResult(
			cache.TryGetValue(key, out TItem? result) ? result : null
		);

	public Task SetAsync<TItem>(TKey key, TItem item)
	{
		cache.Set(key, item);
		return Task.CompletedTask;
	}

	public Task RemoveAsync(TKey key)
	{
		cache.Remove(key);
		return Task.CompletedTask;
	}
}
