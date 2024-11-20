namespace CustomCADs.Orders.Endpoints.Orders.Post.PresignedUrl;

public record GetOrderPresigendUrlRequest(
    string OrderName,
    string ContentType,
    string FileName
);
