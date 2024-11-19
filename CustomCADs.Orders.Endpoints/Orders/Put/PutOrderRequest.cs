namespace CustomCADs.Orders.Endpoints.Orders.Put;

public record PutOrderRequest(
    Guid Id,
    string Name,
    string Description
);
