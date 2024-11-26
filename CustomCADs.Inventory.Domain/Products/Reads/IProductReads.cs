using CustomCADs.Inventory.Domain.Products.Enums;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Account;

namespace CustomCADs.Inventory.Domain.Products.Reads;

public interface IProductReads
{
    Task<Result<Product>> AllAsync(ProductQuery query, bool track = true, CancellationToken ct = default);
    Task<Product?> SingleByIdAsync(ProductId id, bool track = true, CancellationToken ct = default);
    Task<bool> ExistsByIdAsync(ProductId id, CancellationToken ct = default);
    Task<int> CountByStatusAsync(UserId creatorId, ProductStatus status, CancellationToken ct = default);
}
