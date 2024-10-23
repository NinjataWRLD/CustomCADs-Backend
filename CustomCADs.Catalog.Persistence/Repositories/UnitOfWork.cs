using CustomCADs.Shared.Domain;

namespace CustomCADs.Catalog.Persistence.Repositories;

public class UnitOfWork(CatalogContext cadContext) : IUnitOfWork
{
    public async Task SaveChangesAsync(CancellationToken ct = default)
        => await cadContext.SaveChangesAsync(ct).ConfigureAwait(false);
}
