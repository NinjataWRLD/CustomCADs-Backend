namespace CustomCADs.Catalog.Domain.Products.Reads;

public class ProductResult
{
    public int Count { get; set; }
    public ICollection<Product> Products { get; set; } = [];
}
