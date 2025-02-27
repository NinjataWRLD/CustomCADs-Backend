namespace CustomCADs.Carts.Application.PurchasedCarts.Queries.GetAll;

public record GetAllPurchasedCartsDto(
    PurchasedCartId Id,
    decimal Total,
    DateTime PurchaseDate,
    int ItemsCount
);