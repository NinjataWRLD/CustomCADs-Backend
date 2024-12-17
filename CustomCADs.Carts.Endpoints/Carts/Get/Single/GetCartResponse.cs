using CustomCADs.Carts.Endpoints.Common.Dtos;

namespace CustomCADs.Carts.Endpoints.Carts.Get.Single;

public sealed record GetCartResponse(
    Guid Id,
    decimal Total,
    string PurchaseDate,
    Guid BuyerId,
    Guid? ShipmentId,
    ICollection<CartItemResponse> Items
);
