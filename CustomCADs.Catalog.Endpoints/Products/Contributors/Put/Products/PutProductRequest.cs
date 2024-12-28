namespace CustomCADs.Catalog.Endpoints.Products.Contributors.Put.Products;

public sealed record PutProductRequest(
    Guid Id,
    string Name,
    string Description,
    int CategoryId,
    decimal Price,
    string? ImageKey,
    string? ImageContentType,
    string? CadKey,
    string? CadContentType
);
