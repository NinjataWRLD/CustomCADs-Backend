using CustomCADs.Carts.Domain.Repositories;
using CustomCADs.Shared.Core.Bases.Entities;

namespace CustomCADs.Carts.Persistence.Common;

public class Writes<TEntity>(CartsContext context) : IWrites<TEntity>
    where TEntity : BaseAggregateRoot
{
    public async Task<TEntity> AddAsync(TEntity entity, CancellationToken ct = default)
        => (await context.Set<TEntity>().AddAsync(entity, ct).ConfigureAwait(false)).Entity;

    public void Remove(TEntity entity)
        => context.Set<TEntity>().Remove(entity);
}
