namespace CustomCADs.Orders.Application.Orders.Queries.GetImageUrlPost;

public record GetOrderImagePresignedUrlPostDto(
    string GeneratedKey,
    string PresignedUrl
);