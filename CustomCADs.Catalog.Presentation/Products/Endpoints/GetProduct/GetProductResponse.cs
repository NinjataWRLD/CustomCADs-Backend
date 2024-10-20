using CustomCADs.Catalog.Presentation.Categories.Endpoints;

namespace CustomCADs.Catalog.Presentation.Products.Endpoints.GetProduct;

public class GetProductResponse
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public decimal Price { get; set; }
    public required string CadPath { get; set; }
    public CoordinatesDto CamCoordinates { get; set; } = new();
    public CoordinatesDto PanCoordinates { get; set; } = new();
    public required string CreationDate { get; set; }
    public CategoryResponseDto Category { get; set; } = new();
}
