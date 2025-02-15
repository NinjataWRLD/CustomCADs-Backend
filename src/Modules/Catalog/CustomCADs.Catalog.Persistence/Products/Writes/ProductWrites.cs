using CustomCADs.Catalog.Domain.Products.Writes;
using CustomCADs.Catalog.Persistence.ShadowEntities;

namespace CustomCADs.Catalog.Persistence.Products.Writes;

public class ProductWrites(CatalogContext context) : IProductWrites
{
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
}
