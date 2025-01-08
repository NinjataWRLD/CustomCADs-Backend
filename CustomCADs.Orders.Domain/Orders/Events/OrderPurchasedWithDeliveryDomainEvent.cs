using CustomCADs.Shared.Core.Bases.Events;
using CustomCADs.Shared.Core.Common.Dtos;

namespace CustomCADs.Orders.Domain.Orders.Events;

public record OrderPurchasedWithDeliveryDomainEvent(
    OrderId OrderId,
    string ShipmentService,
    double Weight,
    AddressDto Address,
    ContactDto Contact
) : BaseDomainEvent;
