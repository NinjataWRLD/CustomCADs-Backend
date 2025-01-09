using CustomCADs.Shared.Core.Bases.Events;
using CustomCADs.Shared.Core.Common.Dtos;

namespace CustomCADs.Orders.Domain.OngoingOrders.Events;

public record OngoingOrderDeliveryRequestedDomainEvent(
    CompletedOrderId Id,
    string ShipmentService,
    double Weight,
    int Count,
    AddressDto Address,
    ContactDto Contact
) : BaseDomainEvent;
