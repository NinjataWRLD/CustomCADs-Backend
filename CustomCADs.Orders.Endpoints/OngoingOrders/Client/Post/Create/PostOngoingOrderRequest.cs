namespace CustomCADs.Orders.Endpoints.OngoingOrders.Client.Post.Create;

public sealed record PostOngoingOrderRequest(
    string Name,
    string Description
);
