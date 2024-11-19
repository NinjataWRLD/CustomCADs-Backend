namespace CustomCADs.Gallery.Endpoints.Carts.Get;

public record GetCartResponse(
    Guid Id,
    decimal Total,
    string PurchaseDate,
    Guid BuyerId,
    ICollection<CartItemDto> Items
);
