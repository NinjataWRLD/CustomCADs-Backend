﻿using CustomCADs.Shared.Application.Cache;
using Microsoft.Extensions.Caching.Memory;

namespace CustomCADs.Shared.Infrastructure.Cache;

public class MemoryCacheService(IMemoryCache cache)
    : MemoryCacheService<string>(cache), ICacheService;

public class MemoryCacheService<TKey>(IMemoryCache cache) : ICacheService<TKey> where TKey : class
{
    public async Task<TItem?> GetAsync<TItem>(TKey key) where TItem : class?
        => await Task.Run(() =>
            cache.TryGetValue(key, out TItem? result) ? result : null
        ).ConfigureAwait(false);

    public async Task SetAsync<TItem>((TKey Key, TItem Item) value)
        => await Task.Run(() =>
            cache.Set(value.Key, value.Item)
        ).ConfigureAwait(false);

    public async Task RemoveAsync<TItem>(TKey key)
        => await Task.Run(() =>
            cache.Remove(key)
        ).ConfigureAwait(false);
}
