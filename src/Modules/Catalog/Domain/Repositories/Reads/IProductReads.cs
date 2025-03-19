using CustomCADs.Catalog.Domain.Products;
using CustomCADs.Catalog.Domain.Products.Enums;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Catalog.Domain.Repositories.Reads;

public interface IProductReads
{
    Task<Result<Product>> AllAsync(ProductQuery query, bool track = true, CancellationToken ct = default);
    Task<Product?> SingleByIdAsync(ProductId id, bool track = true, CancellationToken ct = default);
    Task<bool> ExistsByIdAsync(ProductId id, CancellationToken ct = default);
    Task<Dictionary<ProductStatus, int>> CountByStatusAsync(AccountId creatorId, CancellationToken ct = default);
}
