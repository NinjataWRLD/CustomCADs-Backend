namespace CustomCADs.Catalog.Domain.Shared;

public interface IWrites<TEntity> where TEntity : class
{
    Task<TEntity> AddAsync(TEntity entity, CancellationToken ct = default);
    void Remove(TEntity entity);
}
