using CustomCADs.Catalog.Endpoints.Categories.Endpoints;

namespace CustomCADs.Catalog.Endpoints.Products.Endpoints.PostProduct;

public class PostProductResponse
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string CreatorName { get; set; }
    public required string CreationDate { get; set; }
    public decimal Price { get; set; }
    public required string CadPath { get; set; }
    public required string ImagePath { get; set; }
    public CoordinatesDto CamCoordinates { get; set; } = new();
    public CoordinatesDto PanCoordinates { get; set; } = new();
    public required string Status { get; set; }
    public CategoryResponseDto Category { get; set; } = new();
}
