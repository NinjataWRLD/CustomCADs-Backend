namespace CustomCADs.Catalog.Endpoints.Products.Creator.Put.PresignedUrl.Image;

public sealed record GetProductPutPresignedUrlRequest(
    Guid Id,
    string ContentType,
    string FileName
);
