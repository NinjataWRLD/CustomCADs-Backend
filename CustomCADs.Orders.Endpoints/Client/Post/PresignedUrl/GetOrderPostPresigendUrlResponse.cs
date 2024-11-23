namespace CustomCADs.Orders.Endpoints.Client.Post.PresignedUrl;

public record GetOrderPostPresigendUrlResponse(
    string GeneratedImageKey,
    string PresignedImageUrl
);