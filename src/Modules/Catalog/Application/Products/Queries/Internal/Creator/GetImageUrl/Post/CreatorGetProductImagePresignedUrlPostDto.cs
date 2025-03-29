namespace CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.GetImageUrl.Post;

public record CreatorGetProductImagePresignedUrlPostDto(
    string GeneratedKey,
    string PresignedUrl
);
