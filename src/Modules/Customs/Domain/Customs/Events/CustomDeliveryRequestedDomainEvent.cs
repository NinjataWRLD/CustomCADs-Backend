using CustomCADs.Shared.Core.Bases.Events;
using CustomCADs.Shared.Core.Common.Dtos;

namespace CustomCADs.Customs.Domain.Customs.Events;

public record CustomDeliveryRequestedDomainEvent(
    CustomId Id,
    string ShipmentService,
    double Weight,
    int Count,
    AddressDto Address,
    ContactDto Contact
) : BaseDomainEvent;
