namespace CustomCADs.Catalog.Domain.Products.Writes;

public interface IProductWrites
{
    Task AddTagAsync(ProductId id, TagId tagId, CancellationToken ct = default);
    void RemoveTag(ProductId id, TagId tagId);
}
