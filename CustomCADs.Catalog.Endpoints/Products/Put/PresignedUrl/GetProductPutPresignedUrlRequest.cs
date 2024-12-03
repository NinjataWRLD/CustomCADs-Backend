namespace CustomCADs.Catalog.Endpoints.Products.Put.PresignedUrl;

public sealed record GetProductPutPresignedUrlRequest(
    Guid Id,
    string ContentType,
    string FileName
);
