using CustomCADs.Orders.Domain.Carts.Entities;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Account;

namespace CustomCADs.Orders.Domain.Carts.Reads;

public interface ICartReads
{
    Task<CartResult> AllAsync(CartQuery query, bool track = true, CancellationToken ct = default);
    Task<Cart?> SingleByIdAsync(CartId id, bool track = true, CancellationToken ct = default);
    Task<bool> ExistsByIdAsync(CartId id, CancellationToken ct = default);
    Task<int> CountAsync(UserId buyerId, CancellationToken ct = default);
}
