using CustomCADs.Shared.Core.Domain;

namespace CustomCADs.Account.Domain.Shared;

public interface IWrites<TEntity> where TEntity : class, IAggregateRoot
{
    Task<TEntity> AddAsync(TEntity entity, CancellationToken ct = default);
    void Remove(TEntity entity);
}
