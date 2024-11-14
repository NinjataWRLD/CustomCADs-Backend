using CustomCADs.Catalog.Domain.Products.Enums;
using CustomCADs.Shared.Core.Domain.Enums;

namespace CustomCADs.Catalog.Endpoints.Products.GetProducts;

public record GetProductsRequest(
    ProductSortingType SortingType = ProductSortingType.UploadDate,
    SortingDirection SortingDirection = SortingDirection.Descending,
    string? Name = default,
    int Page = 1,
    int Limit = 20
);
