namespace CustomCADs.Catalog.Endpoints.Products.Post;

public record PostProductRequest(
    string Name,
    string Description,
    int CategoryId,
    decimal Price,
    IFormFile File,
    IFormFile Image
);
