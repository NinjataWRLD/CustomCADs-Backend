using CustomCADs.Shared.Core.Bases.Events;
using CustomCADs.Shared.Core.Common.Dtos;

namespace CustomCADs.Orders.Domain.OngoingOrders.Events;

public record OngoingOrderPurchasedWithDeliveryDomainEvent(
    CompletedOrderId OrderId,
    string ShipmentService,
    double Weight,
    AddressDto Address,
    ContactDto Contact
) : BaseDomainEvent;
