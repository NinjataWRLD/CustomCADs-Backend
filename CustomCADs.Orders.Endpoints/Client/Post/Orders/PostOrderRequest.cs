using CustomCADs.Orders.Domain.Orders.Enums;

namespace CustomCADs.Orders.Endpoints.Client.Post.Orders;

public record PostOrderRequest(
    DeliveryType DeliveryType,
    string Name,
    string Description
);
