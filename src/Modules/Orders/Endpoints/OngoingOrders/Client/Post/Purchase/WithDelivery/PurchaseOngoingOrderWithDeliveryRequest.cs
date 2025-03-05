using CustomCADs.Shared.Core.Common.Dtos;

namespace CustomCADs.Orders.Endpoints.OngoingOrders.Client.Post.Purchase.WithDelivery;

public sealed record PurchaseOngoingOrderWithDeliveryRequest(
    Guid Id,
    AddressDto Address,
    ContactDto Contact,
    string PaymentMethodId,
    string ShipmentService,
    int Count,
    Guid CustomizationId
);
