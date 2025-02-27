using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;

namespace CustomCADs.Carts.Domain.ActiveCarts.Reads;

public interface IActiveCartReads
{
    Task<Result<ActiveCart>> AllAsync(ActiveCartQuery query, bool track = true, CancellationToken ct = default);
    Task<ActiveCart?> SingleByBuyerIdAsync(AccountId buyerId, bool track = true, CancellationToken ct = default);
    Task<bool> ExistsByBuyerIdAsync(AccountId buyerId, CancellationToken ct = default);
    Task<int> CountByProductIdAsync(ProductId productId, CancellationToken ct = default);
    Task<int> CountItemsByBuyerIdAsync(AccountId buyerId, CancellationToken ct = default);
}
