using CustomCADs.Delivery.Domain.Repositories;
using CustomCADs.Shared.Persistence.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace CustomCADs.Delivery.Persistence.Repositories;

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
