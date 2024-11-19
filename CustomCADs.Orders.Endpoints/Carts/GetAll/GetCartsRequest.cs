using CustomCADs.Orders.Domain.Carts.Enums;
using CustomCADs.Shared.Core.Domain.Enums;

namespace CustomCADs.Orders.Endpoints.Carts.GetAll;

public record GetCartsRequest(
    CartSortingType SortingType = CartSortingType.PurchaseDate,
    SortingDirection SortingDirection = SortingDirection.Descending,
    int Page = 1,
    int Limit = 20
);
