namespace CustomCADs.Orders.Endpoints.Orders.Post;

public record PostOrderResponse(
    Guid Id,
    string Name,
    string Description,
    string OrderDate,
    string DeliveryType,
    string OrderStatus,
    Guid BuyerId
);
