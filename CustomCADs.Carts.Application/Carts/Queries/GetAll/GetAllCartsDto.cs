namespace CustomCADs.Carts.Application.Carts.Queries.GetAll;

public record GetAllCartsDto(
    CartId Id,
    decimal Total,
    DateTime PurchaseDate,
    int ItemsCount
);