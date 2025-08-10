using CustomCADs.Shared.Application.Dtos.Delivery;

namespace CustomCADs.Carts.Endpoints.ActiveCarts.Endpoints.Post.PurchaseWithDelivery;

public sealed record PurchaseActiveCartRequest(
	string PaymentMethodId,
	string ShipmentService,
	AddressDto Address,
	ContactDto Contact
);
