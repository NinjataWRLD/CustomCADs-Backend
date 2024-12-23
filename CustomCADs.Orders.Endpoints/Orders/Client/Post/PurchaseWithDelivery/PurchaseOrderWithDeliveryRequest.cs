using CustomCADs.Shared.Core.Common.Dtos;

namespace CustomCADs.Orders.Endpoints.Orders.Client.Post.PurchaseWithDelivery;

public sealed record PurchaseOrderWithDeliveryRequest(
    Guid OrderId,
    AddressDto Address,
    ContactDto Contact,
    string PaymentMethodId,
    double Weight
);
