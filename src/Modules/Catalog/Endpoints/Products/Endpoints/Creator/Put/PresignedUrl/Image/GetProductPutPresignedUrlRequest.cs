namespace CustomCADs.Catalog.Endpoints.Products.Endpoints.Creator.Put.PresignedUrl.Image;

public sealed record GetProductPutPresignedUrlRequest(
    Guid Id,
    string ContentType,
    string FileName
);
