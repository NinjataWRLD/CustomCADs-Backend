namespace CustomCADs.Carts.Endpoints.PurchasedCarts.Endpoints.Get.Single;

public sealed record GetPurchasedCartResponse(
    Guid Id,
    decimal Total,
    DateTimeOffset PurchasedAt,
    string BuyerName,
    Guid? ShipmentId,
    ICollection<PurchasedCartItemResponse> Items
);
