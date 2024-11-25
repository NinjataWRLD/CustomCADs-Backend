using CustomCADs.Gallery.Domain.Carts.Entities;
using CustomCADs.Shared.Core.Common.TypedIds.Account;
using CustomCADs.Shared.Core.Common.TypedIds.Gallery;

namespace CustomCADs.Gallery.Domain.Carts.Reads;

public interface ICartReads
{
    Task<CartResult> AllAsync(CartQuery query, bool track = true, CancellationToken ct = default);
    Task<Cart?> SingleByIdAsync(CartId id, bool track = true, CancellationToken ct = default);
    Task<ICollection<CartItem>> ItemsByIdAsync(CartId id, CancellationToken ct = default);
    Task<bool> ExistsByIdAsync(CartId id, CancellationToken ct = default);
    Task<int> CountAsync(UserId buyerId, CancellationToken ct = default);
}
