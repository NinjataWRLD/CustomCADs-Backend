using Microsoft.AspNetCore.Http;

namespace CustomCADs.Catalog.Endpoints.Products.Endpoints.PostProduct;

public class PostProductRequest
{
    public required IFormFile File { get; set; }
    public required IFormFile Image { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required int CategoryId { get; set; }
    public required decimal Cost { get; set; }
}
