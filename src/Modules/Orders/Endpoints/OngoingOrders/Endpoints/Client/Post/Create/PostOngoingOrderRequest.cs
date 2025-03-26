namespace CustomCADs.Orders.Endpoints.OngoingOrders.Endpoints.Client.Post.Create;

public sealed record PostOngoingOrderRequest(
    string Name,
    string Description,
    bool Delivery
);
