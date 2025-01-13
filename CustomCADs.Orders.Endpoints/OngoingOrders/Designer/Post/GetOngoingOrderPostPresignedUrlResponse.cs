namespace CustomCADs.Orders.Endpoints.OngoingOrders.Designer.Post;

public record GetOngoingOrderPostPresignedUrlResponse(
    string CadKey,
    string CadUrl
);