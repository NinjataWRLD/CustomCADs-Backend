namespace CustomCADs.Orders.Endpoints.Orders.Post.PresignedUrl;

public record GetOrderPresigendUrlResponse(
    string GeneratedImageKey,
    string PresignedImageUrl
);