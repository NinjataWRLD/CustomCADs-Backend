using CustomCADs.Catalog.Endpoints.Products.Dtos;

namespace CustomCADs.Catalog.Endpoints.Products.Endpoints.Designer.Get.Validated;

public sealed record GetValidatedProductsResponse(
    Guid Id,
    string Name,
    string UploadedAt,
    string CreatorName,
    CategoryResponse Category
);