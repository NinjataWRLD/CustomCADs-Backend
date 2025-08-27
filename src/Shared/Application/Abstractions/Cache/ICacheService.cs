namespace CustomCADs.Shared.Application.Abstractions.Cache;

public interface ICacheService : ICacheService<string>;
public interface ICacheService<TKey> where TKey : notnull
{
	Task<TItem?> GetOrCreateAsync<TItem>(TKey key, Func<Task<TItem>> factory, Expiration? expiration = null);
	Task<TItem?> GetAsync<TItem>(TKey key) where TItem : class?;
	Task SetAsync<TItem>(TKey key, TItem item);
	Task RemoveAsync(TKey key);
}
