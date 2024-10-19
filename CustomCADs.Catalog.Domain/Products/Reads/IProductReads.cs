using CustomCADs.Catalog.Domain.Products.Enums;

namespace CustomCADs.Catalog.Domain.Products.Reads;

public interface IProductReads
{
    Task<ProductResult> AllAsync(ProductQuery query, bool track = true, CancellationToken ct = default);
    Task<Product?> SingleByIdAsync(Guid id, bool track = true, CancellationToken ct = default);
    Task<bool> ExistsByIdAsync(Guid id, CancellationToken ct = default);
    Task<int> CountAsync(Guid creatorId, ProductStatus status, CancellationToken ct = default);
}
