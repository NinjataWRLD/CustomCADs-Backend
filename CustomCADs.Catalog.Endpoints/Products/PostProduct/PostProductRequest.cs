namespace CustomCADs.Catalog.Endpoints.Products.PostProduct;

public record PostProductRequest(string Name, string Description, int CategoryId, decimal Cost, IFormFile File, IFormFile Image);
