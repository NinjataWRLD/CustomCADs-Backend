namespace CustomCADs.Carts.Endpoints.PurchasedCarts.Get.Single;

public sealed record GetPurchasedCartResponse(
    Guid Id,
    decimal Total,
    string PurchaseDate,
    Guid BuyerId,
    Guid? ShipmentId,
    ICollection<PurchasedCartItemResponse> Items
);
