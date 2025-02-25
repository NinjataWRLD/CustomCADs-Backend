namespace CustomCADs.Orders.Endpoints.OngoingOrders.Client.Post.Create;

public sealed record PostOngoingOrderResponse(
    Guid Id,
    string Name,
    string Description,
    string OrderDate,
    string OrderStatus,
    bool Delivery
);
