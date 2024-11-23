namespace CustomCADs.Inventory.Endpoints.Products.Put.PresignedUrl;

public record GetProductPutPresignedUrlRequest(
    Guid Id,
    string ContentType,
    string FileName
);
