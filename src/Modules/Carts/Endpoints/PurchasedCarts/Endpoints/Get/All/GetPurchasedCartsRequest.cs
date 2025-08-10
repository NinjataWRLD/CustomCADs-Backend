using CustomCADs.Carts.Domain.PurchasedCarts.Enums;
using CustomCADs.Shared.Domain.Enums;

namespace CustomCADs.Carts.Endpoints.PurchasedCarts.Endpoints.Get.All;

public sealed record GetPurchasedCartsRequest(
	PurchasedCartSortingType SortingType = PurchasedCartSortingType.PurchasedAt,
	SortingDirection SortingDirection = SortingDirection.Descending,
	PaymentStatus? PaymentStatus = null,
	int Page = 1,
	int Limit = 20
);
