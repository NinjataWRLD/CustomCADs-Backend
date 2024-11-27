using CustomCADs.Inventory.Domain.Common;
using CustomCADs.Shared.Core.Common.Exceptions;

namespace CustomCADs.Inventory.Persistence.Common;

public class UnitOfWork(InventoryContext context) : IUnitOfWork
{
    public async Task SaveChangesAsync(CancellationToken ct = default)
    {
        try
        {
            await context.SaveChangesAsync(ct).ConfigureAwait(false);
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw DatabaseConflictException.Custom(ex.Message);
        }
        catch (DbUpdateException ex)
        {
            throw DatabaseException.Custom(ex.Message);
        }
    }
}
