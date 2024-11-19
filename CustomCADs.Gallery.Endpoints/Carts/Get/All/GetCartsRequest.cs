using CustomCADs.Gallery.Domain.Carts.Enums;
using CustomCADs.Shared.Core.Domain.Enums;

namespace CustomCADs.Gallery.Endpoints.Carts.Get.All;

public record GetCartsRequest(
    CartSortingType SortingType = CartSortingType.PurchaseDate,
    SortingDirection SortingDirection = SortingDirection.Descending,
    int Page = 1,
    int Limit = 20
);
