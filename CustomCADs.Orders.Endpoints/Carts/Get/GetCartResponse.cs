namespace CustomCADs.Orders.Endpoints.Carts.Get;

public record GetCartResponse(
    Guid Id,
    decimal Total,
    string PurchaseDate,
    Guid BuyerId,
    ICollection<GalleryOrderDto> Orders
);
