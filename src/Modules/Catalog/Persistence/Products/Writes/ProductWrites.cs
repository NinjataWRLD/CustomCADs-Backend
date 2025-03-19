using CustomCADs.Catalog.Domain.Products;
using CustomCADs.Catalog.Domain.Repositories.Writes;
using CustomCADs.Catalog.Persistence.ShadowEntities;

namespace CustomCADs.Catalog.Persistence.Products.Writes;

public class ProductWrites(CatalogContext context) : IProductWrites
{
    public async Task<Product> AddAsync(Product product, CancellationToken ct = default)
        => (await context.Products.AddAsync(product, ct).ConfigureAwait(false)).Entity;

    public async Task AddTagAsync(ProductId id, TagId tagId, CancellationToken ct = default)
    {
        var entity = ProductTag.Create(id, tagId);
        await context.ProductTags.AddAsync(entity, ct).ConfigureAwait(false);
    }

    public void RemoveTag(ProductId id, TagId tagId)
    {
        var entity = ProductTag.Create(id, tagId);
        context.ProductTags.Remove(entity);
    }

    public void Remove(Product product)
        => context.Remove(product);
}
