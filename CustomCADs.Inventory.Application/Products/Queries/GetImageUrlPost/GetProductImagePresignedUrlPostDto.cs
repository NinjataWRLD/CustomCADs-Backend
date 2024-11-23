namespace CustomCADs.Inventory.Application.Products.Queries.GetImageUrlPost;

public record GetProductImagePresignedUrlPostDto(
    string GeneratedKey,
    string PresignedUrl
);
