using CustomCADs.Shared.Application.Dtos.Delivery;
using CustomCADs.Shared.Domain.Bases.Events;

namespace CustomCADs.Customs.Application.Customs.Events.Application.DeliveryRequested;

public record CustomDeliveryRequestedApplicationEvent(
	CustomId Id,
	string ShipmentService,
	double Weight,
	int Count,
	AddressDto Address,
	ContactDto Contact
) : BaseApplicationEvent;
