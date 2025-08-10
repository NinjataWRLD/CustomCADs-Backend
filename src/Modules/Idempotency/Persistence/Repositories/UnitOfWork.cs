using CustomCADs.Idempotency.Domain.Repositories;
using CustomCADs.Shared.Persistence.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace CustomCADs.Idempotency.Persistence.Repositories;

public class UnitOfWork(IdempotencyContext context) : IUnitOfWork
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

	public async Task ClearIdempotencyKeysAsync(DateTimeOffset before, CancellationToken ct = default)
		=> await context.IdempotencyKeys
			.Where(x => x.CreatedAt < before)
			.ExecuteDeleteAsync(ct)
			.ConfigureAwait(false);
}
