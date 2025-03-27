namespace CustomCADs.Orders.Endpoints.OngoingOrders.Endpoints.Client.Post.Create;

public sealed record PostOngoingOrderResponse(
    Guid Id,
    string Name,
    string Description,
    string OrderedAt,
    string OrderStatus,
    bool Delivery
);
