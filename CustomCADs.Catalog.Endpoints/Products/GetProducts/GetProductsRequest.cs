namespace CustomCADs.Catalog.Endpoints.Products.GetProducts;

public class GetProductsRequest
{
    public string? Sorting { get; set; }
    public string? Category { get; set; }
    public string? Name { get; set; }
    public int Page { get; set; } = 1;
    public int Limit { get; set; } = 20;
}
