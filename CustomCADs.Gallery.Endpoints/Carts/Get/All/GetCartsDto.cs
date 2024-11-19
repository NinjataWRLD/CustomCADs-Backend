namespace CustomCADs.Gallery.Endpoints.Carts.Get.All;

public record GetCartsDto(
    Guid Id,
    decimal Total,
    string PurchaseDate,
    int ItemsCount
);