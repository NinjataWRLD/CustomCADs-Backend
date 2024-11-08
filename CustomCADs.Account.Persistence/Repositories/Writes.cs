using CustomCADs.Account.Domain.Shared;
using CustomCADs.Shared.Core.Entities;

namespace CustomCADs.Account.Persistence.Repositories;

public class Writes<TEntity>(AccountContext context) : IWrites<TEntity> where TEntity : class, IEntity
{
    public async Task<TEntity> AddAsync(TEntity entity, CancellationToken ct = default)
        => (await context.Set<TEntity>().AddAsync(entity, ct).ConfigureAwait(false)).Entity;

    public void Remove(TEntity entity)
        => context.Set<TEntity>().Remove(entity);
}
