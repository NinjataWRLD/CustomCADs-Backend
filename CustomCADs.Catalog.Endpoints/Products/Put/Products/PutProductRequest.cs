namespace CustomCADs.Catalog.Endpoints.Products.Put.Products;

public sealed record PutProductRequest(
    Guid Id,
    string Name,
    string Description,
    int CategoryId,
    decimal Price,
    string? ImageKey
);
