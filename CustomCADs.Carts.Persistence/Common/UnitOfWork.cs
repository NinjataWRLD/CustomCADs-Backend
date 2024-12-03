using CustomCADs.Carts.Domain.Common;
using CustomCADs.Shared.Core.Common.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace CustomCADs.Carts.Persistence.Common;

public class UnitOfWork(GalleryContext context) : IUnitOfWork
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
