using CustomCADs.Inventory.Domain.Common;
using CustomCADs.Shared.Core.Bases.Entities;

namespace CustomCADs.Inventory.Persistence.Common;

public class Writes<TEntity>(InventoryContext context) : IWrites<TEntity> where TEntity : BaseAggregateRoot
{
    public async Task<TEntity> AddAsync(TEntity entity, CancellationToken ct = default)
        => (await context.Set<TEntity>().AddAsync(entity, ct).ConfigureAwait(false)).Entity;

    public void Remove(TEntity entity)
        => context.Set<TEntity>().Remove(entity);
}
