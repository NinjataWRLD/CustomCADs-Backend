using CustomCADs.Shared.Domain;

namespace CustomCADs.Catalog.Persistence.Repositories;

public class UnitOfWork(CatalogContext cadContext) : IUnitOfWork
{
    public async Task SaveChangesAsync()
        => await cadContext.SaveChangesAsync().ConfigureAwait(false);
}
