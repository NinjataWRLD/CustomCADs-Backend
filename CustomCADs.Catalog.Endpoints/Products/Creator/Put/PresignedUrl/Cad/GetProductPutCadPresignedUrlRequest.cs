namespace CustomCADs.Catalog.Endpoints.Products.Creator.Put.PresignedUrl.Cad;

public sealed record GetProductPutCadPresignedUrlRequest(
    Guid Id,
    string ContentType,
    string FileName
);
