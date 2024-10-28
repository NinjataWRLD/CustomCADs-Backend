using CustomCADs.Catalog.Endpoints.Categories.Endpoints;

namespace CustomCADs.Catalog.Endpoints.Products.Endpoints.GetProducts;

public class GetProductsDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string CreatorName { get; set; }
    public required string UploadDate { get; set; }
    public required string ImagePath { get; set; }
    public CategoryResponseDto Category { get; set; } = new();
}
