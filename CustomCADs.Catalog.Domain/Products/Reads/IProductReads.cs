using CustomCADs.Catalog.Domain.Products.Enums;

namespace CustomCADs.Catalog.Domain.Products.Reads;

public interface IProductReads
{
    Task<IEnumerable<Product>> AllAsync(bool asNoTracking = false, CancellationToken ct = default);
    Task<Product?> SingleByIdAsync(Guid id, bool asNoTracking = false, CancellationToken ct = default);
    Task<bool> ExistsByIdAsync(bool exists, CancellationToken ct = default);
    Task<bool> CountAsync(string creatorName, CadStatus status, CancellationToken ct = default);
}
