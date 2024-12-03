namespace CustomCADs.Orders.Endpoints.Orders.Client.Put;

public sealed record PutOrderRequest(
    Guid Id,
    string Name,
    string Description
);
