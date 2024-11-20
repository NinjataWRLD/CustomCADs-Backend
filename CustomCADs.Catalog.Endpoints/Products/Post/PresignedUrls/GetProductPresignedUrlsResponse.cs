namespace CustomCADs.Catalog.Endpoints.Products.Post.PresignedUrls;

public record GetProductPresignedUrlsResponse(
    string GeneratedImageKey,
    string PresignedImageUrl,
    string GeneratedCadKey,
    string PresignedCadUrl
);
