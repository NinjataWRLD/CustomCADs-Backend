using CustomCADs.Catalog.Endpoints.Categories.Endpoints;

namespace CustomCADs.Catalog.Endpoints.Products.Endpoints.GetProducts;

public class GetProductsDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string CreatorName { get; set; }
    public required string CreationDate { get; set; }
    public required string ImagePath { get; set; }
    public CategoryResponseDto Category { get; set; } = new();
}
