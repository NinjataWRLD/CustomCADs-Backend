namespace CustomCADs.Orders.Endpoints.Orders.Put.PresignedUrl;

public record GetOrderPutPresignedUrlRequest(
    Guid Id,
    string ContentType,
    string FileName
);