using CustomCADs.Shared.Core.Bases.Entities;

namespace CustomCADs.Orders.Domain.Repositories;

public interface IWrites<TEntity> where TEntity : BaseAggregateRoot
{
    Task<TEntity> AddAsync(TEntity entity, CancellationToken ct = default);
    void Remove(TEntity entity);
    void RemoveRange(params IEnumerable<TEntity> entity);
}
