using CustomCADs.Shared.Core.Bases.Entities;

namespace CustomCADs.Idempotency.Domain.Repositories;

public interface IWrites<TEntity> where TEntity : BaseAggregateRoot
{
	Task<TEntity> AddAsync(TEntity entity, CancellationToken ct = default);
	void Remove(TEntity entity);
}
