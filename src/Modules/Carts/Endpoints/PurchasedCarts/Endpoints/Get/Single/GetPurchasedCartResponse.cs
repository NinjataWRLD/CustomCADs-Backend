using CustomCADs.Carts.Endpoints.PurchasedCarts.Dtos;

namespace CustomCADs.Carts.Endpoints.PurchasedCarts.Endpoints.Get.Single;

public sealed record GetPurchasedCartResponse(
    Guid Id,
    decimal Total,
    string PurchaseDate,
    string BuyerName,
    Guid? ShipmentId,
    ICollection<PurchasedCartItemResponse> Items
);
