using CustomCADs.Shared.Core.Common.Dtos;

namespace CustomCADs.Carts.Endpoints.Carts.Post.PurchaseWithDelivery;

public sealed record PurchaseCartRequest(
    Guid CartId,
    AddressDto Address,
    ContactDto Contact,
    string PaymentMethodId,
    string ShipmentService
);
