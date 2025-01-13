namespace CustomCADs.Orders.Endpoints.OngoingOrders.Designer.Post;

public sealed record GetOngoingOrderPostPresignedUrlRequest(
    Guid Id,
    string ContentType,
    string FileName
);