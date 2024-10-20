namespace CustomCADs.Catalog.Presentation.Products.Endpoints.PutProduct;

public class PutProductRequest
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required int CategoryId { get; set; }
    public required decimal Cost { get; set; }
    public IFormFile? Image { get; set; }
}
