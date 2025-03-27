namespace CustomCADs.Catalog.Application.Products.Queries.Internal.Shared.GetImageUrl.Post;

public record GetProductImagePresignedUrlPostDto(
    string GeneratedKey,
    string PresignedUrl
);
