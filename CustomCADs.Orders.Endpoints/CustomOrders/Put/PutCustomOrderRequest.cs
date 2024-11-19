namespace CustomCADs.Orders.Endpoints.CustomOrders.Put;

public record PutCustomOrderRequest(
    Guid Id,
    string Name,
    string Description
);
