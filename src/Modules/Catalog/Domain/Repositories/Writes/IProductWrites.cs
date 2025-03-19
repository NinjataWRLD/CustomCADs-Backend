using CustomCADs.Catalog.Domain.Products;

namespace CustomCADs.Catalog.Domain.Repositories.Writes;

public interface IProductWrites
{
    Task<Product> AddAsync(Product product, CancellationToken ct = default);
    Task AddTagAsync(ProductId id, TagId tagId, CancellationToken ct = default);
    void RemoveTag(ProductId id, TagId tagId);
    void Remove(Product product);
}
