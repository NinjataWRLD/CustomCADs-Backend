using CustomCADs.Inventory.Domain.Products.ValueObjects;

namespace CustomCADs.Inventory.Endpoints.Gallery.Get.All;

public sealed record GetAllGaleryProductsRequest(
    int? CategoryId = null,
    string? Name = null,
    ProductSorting? Sorting = null,
    int Page = 1,
    int Limit = 20
);
