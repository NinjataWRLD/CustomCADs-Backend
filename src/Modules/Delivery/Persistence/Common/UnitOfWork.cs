using CustomCADs.Delivery.Domain.Common;
using CustomCADs.Shared.Core.Common.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace CustomCADs.Delivery.Persistence.Common;

public class UnitOfWork(DeliveryContext context) : IUnitOfWork
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
