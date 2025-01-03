using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;

namespace CustomCADs.Carts.Domain.Carts.Reads;

public interface ICartReads
{
    Task<Result<Cart>> AllAsync(CartQuery query, bool track = true, CancellationToken ct = default);
    Task<Cart?> SingleByIdAsync(CartId id, bool track = true, CancellationToken ct = default);
    Task<bool> ExistsByIdAsync(CartId id, CancellationToken ct = default);
    Task<int> CountByProductIdAsync(ProductId productId, CancellationToken ct = default);
    Task<int> CountByBuyerIdAsync(AccountId buyerId, CancellationToken ct = default);
    Task<Dictionary<CartId, int>> CountItemsByBuyerIdAsync(AccountId buyerId, CancellationToken ct = default);
}
