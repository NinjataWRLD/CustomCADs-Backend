namespace CustomCADs.Orders.Endpoints.Client.Post;

public record PostOrderResponse(
    Guid Id,
    string Name,
    string Description,
    string OrderDate,
    string DeliveryType,
    string OrderStatus
);
