namespace CustomCADs.Shared.Application.Cache;

public interface ICacheService : ICacheService<string>;
public interface ICacheService<TKey> where TKey : class
{
    Task<TItem?> GetAsync<TItem>(TKey key) where TItem : class?;
    Task SetAsync<TItem>((TKey Key, TItem Item) value);
    Task SetRangeAsync<TItem>(params (TKey Key, TItem Item)[] values);
    Task RemoveAsync<TItem>(TKey key);
    Task RemoveRangeAsync<TItem>(params TKey[] key);
}
