namespace CustomCADs.Gallery.Endpoints.Carts.GetAll;

public record GetCartsDto(
    Guid Id,
    decimal Total,
    string PurchaseDate,
    int ItemsCount
);