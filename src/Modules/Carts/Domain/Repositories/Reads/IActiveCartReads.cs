using CustomCADs.Carts.Domain.ActiveCarts;
using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.TypedIds.Catalog;

namespace CustomCADs.Carts.Domain.Repositories.Reads;

public interface IActiveCartReads
{
	Task<ActiveCartItem[]> AllAsync(ProductId productId, bool track = true, CancellationToken ct = default);
	Task<ActiveCartItem[]> AllAsync(AccountId buyerId, bool track = true, CancellationToken ct = default);
	Task<ActiveCartItem?> SingleAsync(AccountId buyerId, ProductId productId, bool track = true, CancellationToken ct = default);
	Task<bool> ExistsAsync(AccountId buyerId, CancellationToken ct = default);
	Task<int> CountAsync(AccountId buyerId, CancellationToken ct = default);
}
