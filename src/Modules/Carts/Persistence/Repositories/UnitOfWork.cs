using CustomCADs.Carts.Domain.Repositories;
using CustomCADs.Shared.Core.Common.Exceptions;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using Microsoft.EntityFrameworkCore;

namespace CustomCADs.Carts.Persistence.Repositories;

public class UnitOfWork(CartsContext context) : IUnitOfWork
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

    public async Task BulkDeleteItemsByProductIdAsync(ProductId id, CancellationToken ct = default)
    {
        try
        {
            await context.ActiveCartItems
                .Where(item => item.ProductId == id)
                .ExecuteDeleteAsync(ct)
                .ConfigureAwait(false);
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
