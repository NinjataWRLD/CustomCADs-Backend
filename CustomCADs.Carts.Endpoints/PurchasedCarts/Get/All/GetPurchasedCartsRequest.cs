using CustomCADs.Carts.Domain.PurchasedCarts.Enums;
using CustomCADs.Shared.Core.Common.Enums;

namespace CustomCADs.Carts.Endpoints.PurchasedCarts.Get.All;

public sealed record GetPurchasedCartsRequest(
    PurchasedCartSortingType SortingType = PurchasedCartSortingType.PurchaseDate,
    SortingDirection SortingDirection = SortingDirection.Descending,
    int Page = 1,
    int Limit = 20
);
