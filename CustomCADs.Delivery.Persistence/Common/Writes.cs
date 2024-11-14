using CustomCADs.Delivery.Domain.Common;
using CustomCADs.Shared.Core.Domain;

namespace CustomCADs.Delivery.Persistence.Common;

public class Writes<TEntity>(DeliveryContext context) : IWrites<TEntity>
    where TEntity : BaseAggregateRoot
{
    public async Task<TEntity> AddAsync(TEntity entity, CancellationToken ct = default)
        => (await context.Set<TEntity>().AddAsync(entity, ct).ConfigureAwait(false)).Entity;

    public void Remove(TEntity entity)
        => context.Set<TEntity>().Remove(entity);
}
