using CustomCADs.Catalog.Endpoints.Categories;

namespace CustomCADs.Catalog.Endpoints.Products.RecentProducts;

public class RecentProductsResponse
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Status { get; set; }
    public required string UploadDate { get; set; }
    public CategoryResponseDto Category { get; set; } = new();
}
