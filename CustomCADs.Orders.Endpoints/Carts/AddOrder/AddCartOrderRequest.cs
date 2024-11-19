using CustomCADs.Orders.Domain.Common.Enums;

namespace CustomCADs.Orders.Endpoints.Carts.AddOrder;

public record AddCartOrderRequest(
    Guid CartId,
    DeliveryType DeliveryType,
    int Quantity,
    Guid ProductId
);
