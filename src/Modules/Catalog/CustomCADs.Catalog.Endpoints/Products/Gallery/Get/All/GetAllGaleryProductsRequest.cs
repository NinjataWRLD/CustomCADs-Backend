using CustomCADs.Catalog.Application.Common.Enums;
using CustomCADs.Shared.Core.Common.Enums;

namespace CustomCADs.Catalog.Endpoints.Products.Gallery.Get.All;

public sealed record GetAllGaleryProductsRequest(
    int? CategoryId = null,
    string? Name = null,
    ProductGallerySortingType SortingType = ProductGallerySortingType.UploadDate,
    SortingDirection SortingDirection = SortingDirection.Descending,
    int Page = 1,
    int Limit = 20
);
