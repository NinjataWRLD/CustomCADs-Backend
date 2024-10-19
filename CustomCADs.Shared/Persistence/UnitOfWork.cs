using CustomCADs.Catalog.Persistence;
using System.Transactions;

namespace CustomCADs.Shared.Persistence;

public class UnitOfWork(CatalogContext cadContext) : IUnitOfWork
{
    public async Task SaveChangesAsync()
    {
        using TransactionScope scope = new(TransactionScopeAsyncFlowOption.Enabled);

        await cadContext.SaveChangesAsync().ConfigureAwait(false);

        scope.Complete();
    }
}
