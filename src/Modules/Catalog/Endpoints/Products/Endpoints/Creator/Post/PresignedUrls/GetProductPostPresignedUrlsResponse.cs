namespace CustomCADs.Catalog.Endpoints.Products.Endpoints.Creator.Post.PresignedUrls;

public sealed record GetProductPostPresignedUrlsResponse(
    string GeneratedImageKey,
    string PresignedImageUrl,
    string GeneratedCadKey,
    string PresignedCadUrl
);
