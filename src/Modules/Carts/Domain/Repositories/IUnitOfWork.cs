using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.TypedIds.Catalog;

namespace CustomCADs.Carts.Domain.Repositories;

public interface IUnitOfWork
{
	Task SaveChangesAsync(CancellationToken ct = default);
	Task BulkDeleteItemsByBuyerIdAsync(AccountId id, CancellationToken ct = default);
	Task BulkDeleteItemsByProductIdAsync(ProductId id, CancellationToken ct = default);
}
