using CustomCADs.Shared.Core.Bases.Events;
using CustomCADs.Shared.Core.Common.Dtos;

namespace CustomCADs.Carts.Domain.ActiveCarts.Events;

public record ActiveCartDeliveryRequestedDomainEvent(
    PurchasedCartId Id,
    string ShipmentService,
    double Weight,
    int Count,
    AddressDto Address,
    ContactDto Contact
) : BaseDomainEvent;
