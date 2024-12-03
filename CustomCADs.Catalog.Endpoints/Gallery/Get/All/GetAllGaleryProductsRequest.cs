using CustomCADs.Catalog.Domain.Products.ValueObjects;

namespace CustomCADs.Catalog.Endpoints.Gallery.Get.All;

public sealed record GetAllGaleryProductsRequest(
    int? CategoryId = null,
    string? Name = null,
    ProductSorting? Sorting = null,
    int Page = 1,
    int Limit = 20
);
