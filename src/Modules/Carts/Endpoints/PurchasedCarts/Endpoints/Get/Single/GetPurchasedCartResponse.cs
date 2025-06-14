using CustomCADs.Carts.Domain.PurchasedCarts.Enums;

namespace CustomCADs.Carts.Endpoints.PurchasedCarts.Endpoints.Get.Single;

public sealed record GetPurchasedCartResponse(
	Guid Id,
	decimal Total,
	DateTimeOffset PurchasedAt,
	PaymentStatus PaymentStatus,
	string BuyerName,
	Guid? ShipmentId,
	ICollection<PurchasedCartItemResponse> Items
);
