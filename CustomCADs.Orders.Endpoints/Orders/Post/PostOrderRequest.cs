using CustomCADs.Orders.Domain.Orders.Enums;

namespace CustomCADs.Orders.Endpoints.Orders.Post;

public record PostOrderRequest(
    DeliveryType DeliveryType,
    string Name,
    string Description
);
