namespace CustomCADs.Orders.Endpoints.OngoingOrders.Endpoints.Client.Put;

public sealed record PutOngoingOrderRequest(
    Guid Id,
    string Name,
    string Description
);
