using CustomCADs.Shared.Application.Dtos.Delivery;
using CustomCADs.Shared.Domain.Bases.Events;

namespace CustomCADs.Carts.Application.ActiveCarts.Events.Application.DeliveryRequested;

public record ActiveCartDeliveryRequestedApplicationEvent(
	PurchasedCartId Id,
	string ShipmentService,
	double Weight,
	int Count,
	AddressDto Address,
	ContactDto Contact
) : BaseApplicationEvent;
