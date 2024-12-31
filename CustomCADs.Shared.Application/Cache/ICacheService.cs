namespace CustomCADs.Shared.Application.Cache;

public interface ICacheService : ICacheService<string>;
public interface ICacheService<TKey> where TKey : class
{
    Task<TItem?> GetAsync<TItem>(TKey key) where TItem : class?;
    Task SetAsync<TItem>(TKey key, TItem item);
    Task RemoveAsync<TItem>(TKey key);
}
