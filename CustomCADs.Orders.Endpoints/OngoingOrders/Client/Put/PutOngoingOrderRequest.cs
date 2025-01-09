namespace CustomCADs.Orders.Endpoints.OngoingOrders.Client.Put;

public sealed record PutOngoingOrderRequest(
    Guid Id,
    string Name,
    string Description
);
