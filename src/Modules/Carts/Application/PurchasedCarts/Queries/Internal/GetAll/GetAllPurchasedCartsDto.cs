namespace CustomCADs.Carts.Application.PurchasedCarts.Queries.Internal.GetAll;

public record GetAllPurchasedCartsDto(
    PurchasedCartId Id,
    decimal Total,
    DateTime PurchaseDate,
    int ItemsCount
);