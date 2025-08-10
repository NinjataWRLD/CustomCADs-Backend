using CustomCADs.Carts.Domain.PurchasedCarts;
using CustomCADs.Shared.Domain.Querying;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Carts.Domain.Repositories.Reads;

public interface IPurchasedCartReads
{
	Task<Result<PurchasedCart>> AllAsync(PurchasedCartQuery query, bool track = true, CancellationToken ct = default);
	Task<PurchasedCart?> SingleByIdAsync(PurchasedCartId id, bool track = true, CancellationToken ct = default);
	Task<int> CountAsync(AccountId buyerId, CancellationToken ct = default);
	Task<Dictionary<PurchasedCartId, int>> CountItemsAsync(AccountId buyerId, CancellationToken ct = default);
}
