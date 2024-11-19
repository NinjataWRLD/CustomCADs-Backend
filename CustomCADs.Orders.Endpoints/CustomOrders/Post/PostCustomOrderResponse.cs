namespace CustomCADs.Orders.Endpoints.CustomOrders.Post;

public record PostCustomOrderResponse(
    Guid Id,
    string Name,
    string Description,
    string OrderDate,
    string DeliveryType,
    string OrderStatus,
    Guid BuyerId
);
