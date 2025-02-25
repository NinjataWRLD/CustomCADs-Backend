namespace CustomCADs.Catalog.Persistence.ShadowEntities;

public class ProductTag
{
    private ProductTag() { }

    public ProductId ProductId { get; set; }
    public TagId TagId { get; set; }

    public static ProductTag Create(ProductId id, TagId tagId)
        => new() { ProductId = id, TagId = tagId };
}
