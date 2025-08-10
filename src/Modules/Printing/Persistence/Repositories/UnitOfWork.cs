using CustomCADs.Printing.Domain.Repositories;
using CustomCADs.Shared.Core.Common.Exceptions.Persistence;

namespace CustomCADs.Printing.Persistence.Repositories;

public class UnitOfWork(PrintingContext context) : IUnitOfWork
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
