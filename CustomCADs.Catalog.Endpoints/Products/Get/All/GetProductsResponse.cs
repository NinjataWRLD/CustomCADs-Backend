using CustomCADs.Catalog.Endpoints.Helpers.Dtos;

namespace CustomCADs.Catalog.Endpoints.Products.Get.All;

public sealed record GetProductsResponse(
    Guid Id,
    string Name,
    string CreatorName,
    string UploadDate,
    ImageDto Image,
    CategoryResponse Category
);
