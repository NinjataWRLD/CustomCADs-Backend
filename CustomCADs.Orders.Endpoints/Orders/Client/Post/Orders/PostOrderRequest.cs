using CustomCADs.Orders.Domain.Orders.Enums;

namespace CustomCADs.Orders.Endpoints.Orders.Client.Post.Orders;

public sealed record PostOrderRequest(
    DeliveryType DeliveryType,
    string Name,
    string Description
);
