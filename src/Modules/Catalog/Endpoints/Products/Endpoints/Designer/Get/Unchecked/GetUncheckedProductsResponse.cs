using CustomCADs.Catalog.Endpoints.Products.Dtos;

namespace CustomCADs.Catalog.Endpoints.Products.Endpoints.Designer.Get.Unchecked;

public sealed record GetUncheckedProductsResponse(
    Guid Id,
    string Name,
    string UploadedAt,
    string CreatorName,
    CategoryResponse Category
);