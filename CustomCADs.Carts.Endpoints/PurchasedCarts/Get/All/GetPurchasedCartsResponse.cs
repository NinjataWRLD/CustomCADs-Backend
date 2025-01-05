namespace CustomCADs.Carts.Endpoints.PurchasedCarts.Get.All;

public sealed record GetPurchasedCartsResponse(
    Guid Id,
    decimal Total,
    string PurchaseDate,
    int ItemsCount
);