using CustomCADs.Gallery.Domain.Carts.Entities;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Account;

namespace CustomCADs.Gallery.Domain.Carts.Reads;

public interface ICartReads
{
    Task<Result<Cart>> AllAsync(CartQuery query, bool track = true, CancellationToken ct = default);
    Task<Cart?> SingleByIdAsync(CartId id, bool track = true, CancellationToken ct = default);
    Task<ICollection<CartItem>> ItemsByIdAsync(CartId id, CancellationToken ct = default);
    Task<bool> ExistsByIdAsync(CartId id, CancellationToken ct = default);
    Task<int> CountAsync(UserId buyerId, CancellationToken ct = default);
}
