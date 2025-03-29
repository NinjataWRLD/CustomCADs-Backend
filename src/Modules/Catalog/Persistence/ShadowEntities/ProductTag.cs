using CustomCADs.Catalog.Domain.Products;
using CustomCADs.Catalog.Domain.Tags;

namespace CustomCADs.Catalog.Persistence.ShadowEntities;

public class ProductTag
{
    private ProductTag() { }

    public ProductId ProductId { get; set; }
    public TagId TagId { get; set; }
    public Product Product { get; set; } = null!;
    public Tag Tag { get; set; } = null!;

    public static ProductTag Create(ProductId id, TagId tagId)
        => new() { ProductId = id, TagId = tagId };
}
