namespace CustomCADs.Catalog.Endpoints.Products.Contributors.Put.PresignedUrl;

public sealed record GetProductPutPresignedUrlRequest(
    Guid Id,
    string ContentType,
    string FileName
);
