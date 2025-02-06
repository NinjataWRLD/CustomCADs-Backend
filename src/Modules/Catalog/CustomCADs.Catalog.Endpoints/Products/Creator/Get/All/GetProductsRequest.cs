using CustomCADs.Catalog.Application.Common.Enums;
using CustomCADs.Shared.Core.Common.Enums;

namespace CustomCADs.Catalog.Endpoints.Products.Creator.Get.All;

public sealed record GetProductsRequest(
    int? CategoryId = default,
    string? Name = default,
    ProductCreatorSortingType SortingType = ProductCreatorSortingType.UploadDate,
    SortingDirection SortingDirection = SortingDirection.Descending,
    int Page = 1,
    int Limit = 20
);
