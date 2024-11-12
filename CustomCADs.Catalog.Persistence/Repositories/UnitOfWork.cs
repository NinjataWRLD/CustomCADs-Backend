using CustomCADs.Catalog.Domain.Common;

namespace CustomCADs.Catalog.Persistence.Repositories;

public class UnitOfWork(CatalogContext context) : IUnitOfWork
{
    public async Task SaveChangesAsync(CancellationToken ct = default)
        => await context.SaveChangesAsync(ct).ConfigureAwait(false);
}
