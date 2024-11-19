namespace CustomCADs.Gallery.Endpoints.Carts.Get.Single;

public record GetCartResponse(
    Guid Id,
    decimal Total,
    string PurchaseDate,
    Guid BuyerId,
    ICollection<CartItemDto> Items
);
