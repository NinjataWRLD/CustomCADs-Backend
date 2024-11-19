namespace CustomCADs.Orders.Endpoints.Carts.GetAll;

public record GetCartsDto(
    Guid Id,
    decimal Total,
    string PurchaseDate,
    int OrdersCount
);