using CustomCADs.Shared.Core.Common.Dtos;

namespace CustomCADs.Orders.Endpoints.Orders.Client.Post.Purchase;

public sealed record PurchaseOrderRequest(
    Guid OrderId,
    AddressDto Address,
    ContactDto Contact,
    string PaymentMethodId
);
