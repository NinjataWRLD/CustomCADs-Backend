using CustomCADs.Gallery.Endpoints.Helpers.Dtos;

namespace CustomCADs.Gallery.Endpoints.Carts.Get.Single;

public sealed record GetCartResponse(
    Guid Id,
    decimal Total,
    string PurchaseDate,
    Guid BuyerId,
    ICollection<CartItemDto> Items
);
