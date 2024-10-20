using CustomCADs.Catalog.Presentation.Categories.Endpoints;

namespace CustomCADs.Catalog.Presentation.Products.Endpoints.GetProducts;

public class GetProductsDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string CreatorName { get; set; }
    public required string CreationDate { get; set; }
    public required string ImagePath { get; set; }
    public CategoryResponseDto Category { get; set; } = new();
}
