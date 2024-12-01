namespace CustomCADs.Orders.Endpoints.Client.Post.Orders;

public record PostOrderResponse(
    Guid Id,
    string Name,
    string Description,
    string OrderDate,
    string DeliveryType,
    string OrderStatus
);
