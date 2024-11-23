namespace CustomCADs.Orders.Endpoints.Client.Put.PresignedUrl;

public record GetOrderPutPresignedUrlRequest(
    Guid Id,
    string ContentType,
    string FileName
);