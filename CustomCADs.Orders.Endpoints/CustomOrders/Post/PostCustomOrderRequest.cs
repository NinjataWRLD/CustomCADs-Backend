using CustomCADs.Orders.Domain.Common.Enums;

namespace CustomCADs.Orders.Endpoints.CustomOrders.Post;

public record PostCustomOrderRequest(
    DeliveryType DeliveryType,
    string Name,
    string Description
);
