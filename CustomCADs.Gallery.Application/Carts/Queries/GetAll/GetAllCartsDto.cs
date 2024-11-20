namespace CustomCADs.Gallery.Application.Carts.Queries.GetAll;

public record GetAllCartsDto(int Count, ICollection<GetAllCartsItem> Carts);

public record GetAllCartsItem(
    CartId Id,
    decimal Total,
    DateTime PurchaseDate,
    int ItemsCount
);