namespace CustomCADs.Catalog.Endpoints.Products.Post.PresignedUrls;

public record GetProductPostPresignedUrlsResponse(
    string GeneratedImageKey,
    string PresignedImageUrl,
    string GeneratedCadKey,
    string PresignedCadUrl
);
