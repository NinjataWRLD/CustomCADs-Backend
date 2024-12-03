using CustomCADs.Gallery.Domain.Carts.Enums;
using CustomCADs.Shared.Core.Common.Enums;

namespace CustomCADs.Gallery.Endpoints.Carts.Get.All;

public sealed record GetCartsRequest(
    CartSortingType SortingType = CartSortingType.PurchaseDate,
    SortingDirection SortingDirection = SortingDirection.Descending,
    int Page = 1,
    int Limit = 20
);
