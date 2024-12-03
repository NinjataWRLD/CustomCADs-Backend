namespace CustomCADs.Inventory.Endpoints.Products.Put.PresignedUrl;

public sealed record GetProductPutPresignedUrlRequest(
    Guid Id,
    string ContentType,
    string FileName
);
