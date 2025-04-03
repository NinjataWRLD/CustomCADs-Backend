using CustomCADs.Shared.Core.Common.Dtos;

namespace CustomCADs.Customs.Endpoints.Customs.Endpoints.Client.Post.Purchase.WithDelivery;

public sealed record PurchasCustomWithDeliveryRequest(
    Guid Id,
    AddressDto Address,
    ContactDto Contact,
    string PaymentMethodId,
    string ShipmentService,
    int Count,
    Guid CustomizationId
);
