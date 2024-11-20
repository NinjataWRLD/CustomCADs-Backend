namespace CustomCADs.Orders.Endpoints.Orders.Post.PresignedUrl;

public record GetOrderPostPresigendUrlRequest(
    string OrderName,
    string ContentType,
    string FileName
);
