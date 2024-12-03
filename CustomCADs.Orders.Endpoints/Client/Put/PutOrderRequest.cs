namespace CustomCADs.Orders.Endpoints.Client.Put;

public sealed record PutOrderRequest(
    Guid Id,
    string Name,
    string Description
);
