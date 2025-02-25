namespace CustomCADs.Catalog.Application.Products.Queries.Shared.GetImageUrl.Post;

public record GetProductImagePresignedUrlPostDto(
    string GeneratedKey,
    string PresignedUrl
);
