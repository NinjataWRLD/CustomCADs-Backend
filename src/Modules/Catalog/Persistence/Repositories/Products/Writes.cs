using CustomCADs.Catalog.Domain.Products;
using CustomCADs.Catalog.Domain.Repositories.Writes;
using CustomCADs.Catalog.Persistence.ShadowEntities;

namespace CustomCADs.Catalog.Persistence.Repositories.Products;

public class Writes(CatalogContext context) : IProductWrites
{
    public async Task<Product> AddAsync(Product product, CancellationToken ct = default)
        => (await context.Products.AddAsync(product, ct).ConfigureAwait(false)).Entity;

    public async Task AddTagAsync(ProductId id, TagId tagId, CancellationToken ct = default)
    {
        var entity = ProductTag.Create(id, tagId);
        await context.ProductTags.AddAsync(entity, ct).ConfigureAwait(false);
    }

    public async Task RemoveTagAsync(ProductId id, TagId tagId)
    {
        ProductTag? productTag = await context.ProductTags
            .FirstOrDefaultAsync(x => x.ProductId == id && x.TagId == tagId)
            .ConfigureAwait(false);

        if (productTag is not null)
            context.ProductTags.Remove(productTag);
    }

    public void Remove(Product product)
        => context.Remove(product);
}
