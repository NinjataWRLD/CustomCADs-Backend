using CustomCADs.Catalog.Domain.Products.Entities;
using CustomCADs.Catalog.Domain.Products.Enums;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Account;

namespace CustomCADs.Catalog.Domain.Products.Reads;

public interface IProductReads
{
    Task<ProductResult> AllAsync(ProductQuery query, bool track = true, CancellationToken ct = default);
    Task<Product?> SingleByIdAsync(ProductId id, bool track = true, CancellationToken ct = default);
    Task<bool> ExistsByIdAsync(ProductId id, CancellationToken ct = default);
    Task<int> CountByStatusAsync(UserId creatorId, ProductStatus status, CancellationToken ct = default);
}
