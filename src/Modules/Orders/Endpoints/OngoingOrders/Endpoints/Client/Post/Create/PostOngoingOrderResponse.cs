namespace CustomCADs.Orders.Endpoints.OngoingOrders.Endpoints.Client.Post.Create;

public sealed record PostOngoingOrderResponse(
    Guid Id,
    string Name,
    string Description,
    string OrderDate,
    string OrderStatus,
    bool Delivery
);
