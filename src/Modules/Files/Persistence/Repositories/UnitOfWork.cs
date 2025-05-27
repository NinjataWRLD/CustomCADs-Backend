using CustomCADs.Files.Domain.Repositories;
using CustomCADs.Shared.Core.Common.Exceptions.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CustomCADs.Files.Persistence.Repositories;

public class UnitOfWork(FilesContext context) : IUnitOfWork
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
