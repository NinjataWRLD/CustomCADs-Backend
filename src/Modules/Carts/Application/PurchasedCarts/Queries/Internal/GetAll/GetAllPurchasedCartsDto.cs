namespace CustomCADs.Carts.Application.PurchasedCarts.Queries.Internal.GetAll;

public record GetAllPurchasedCartsDto(
    PurchasedCartId Id,
    decimal Total,
    DateTimeOffset PurchasedAt,
    int ItemsCount
);