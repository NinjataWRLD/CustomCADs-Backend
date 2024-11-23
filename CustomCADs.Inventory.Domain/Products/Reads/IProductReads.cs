using CustomCADs.Inventory.Domain.Products.Enums;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Account;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Inventory;

namespace CustomCADs.Inventory.Domain.Products.Reads;

public interface IProductReads
{
    Task<ProductResult> AllAsync(ProductQuery query, bool track = true, CancellationToken ct = default);
    Task<Product?> SingleByIdAsync(ProductId id, bool track = true, CancellationToken ct = default);
    Task<bool> ExistsByIdAsync(ProductId id, CancellationToken ct = default);
    Task<int> CountByStatusAsync(UserId creatorId, ProductStatus status, CancellationToken ct = default);
}
