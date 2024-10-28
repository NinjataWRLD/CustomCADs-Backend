using CustomCADs.Catalog.Endpoints.Categories.Endpoints;

namespace CustomCADs.Catalog.Endpoints.Products.Endpoints.GetProduct;

public class GetProductResponse
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public decimal Cost { get; set; }
    public required string CadPath { get; set; }
    public CoordinatesDto CamCoordinates { get; set; } = new();
    public CoordinatesDto PanCoordinates { get; set; } = new();
    public required string CreationDate { get; set; }
    public CategoryResponseDto Category { get; set; } = new();
}
