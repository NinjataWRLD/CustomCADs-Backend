namespace CustomCADs.Orders.Endpoints.Orders.Post.PresignedUrl;

public record GetOrderPostPresigendUrlResponse(
    string GeneratedImageKey,
    string PresignedImageUrl
);