namespace CustomCADs.Orders.Endpoints.Orders.Client.Post.CreateWithDelivery;

public sealed record PostOrderWithDeliveryResponse(
    Guid Id,
    string Name,
    string Description,
    string OrderDate,
    string DeliveryType,
    string OrderStatus
);
