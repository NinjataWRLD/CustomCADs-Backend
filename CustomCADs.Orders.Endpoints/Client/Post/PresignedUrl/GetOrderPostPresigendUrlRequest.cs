namespace CustomCADs.Orders.Endpoints.Client.Post.PresignedUrl;

public record GetOrderPostPresigendUrlRequest(
    string OrderName,
    string ContentType,
    string FileName
);
