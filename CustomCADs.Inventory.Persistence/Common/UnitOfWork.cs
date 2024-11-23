using CustomCADs.Inventory.Domain.Common;

namespace CustomCADs.Inventory.Persistence.Common;

public class UnitOfWork(InventoryContext context) : IUnitOfWork
{
    public async Task SaveChangesAsync(CancellationToken ct = default)
        => await context.SaveChangesAsync(ct).ConfigureAwait(false);
}
