using CustomCADs.Carts.Domain.Repositories;
using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.TypedIds.Catalog;
using CustomCADs.Shared.Persistence.Exceptions;
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

	public async Task BulkDeleteItemsByBuyerIdAsync(AccountId id, CancellationToken ct = default)
	{
		try
		{
			await context.ActiveCartItems
				.Where(item => item.BuyerId == id)
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
