namespace CustomCADs.Orders.Endpoints.Client.Put;

public record PutOrderRequest(
    Guid Id,
    string Name,
    string Description
);
