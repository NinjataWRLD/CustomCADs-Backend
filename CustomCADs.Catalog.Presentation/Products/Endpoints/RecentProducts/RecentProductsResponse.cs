using CustomCADs.Catalog.Presentation.Categories.Endpoints;

namespace CustomCADs.Catalog.Presentation.Products.Endpoints.RecentProducts;

public class RecentProductsResponse
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Status { get; set; }
    public required string CreationDate { get; set; }
    public CategoryResponseDto Category { get; set; } = new();
}
