using CustomCADs.Accounts.Domain.Common;
using CustomCADs.Accounts.Persistence;
using CustomCADs.Shared.Core.Common.Exceptions;

namespace CustomCADs.Accounts.Persistence.Common;

public class UnitOfWork(AccountsContext context) : IUnitOfWork
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
