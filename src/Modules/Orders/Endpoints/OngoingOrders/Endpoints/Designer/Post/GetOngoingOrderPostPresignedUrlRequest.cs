namespace CustomCADs.Orders.Endpoints.OngoingOrders.Endpoints.Designer.Post;

public sealed record GetOngoingOrderPostPresignedUrlRequest(
    Guid Id,
    string ContentType,
    string FileName
);