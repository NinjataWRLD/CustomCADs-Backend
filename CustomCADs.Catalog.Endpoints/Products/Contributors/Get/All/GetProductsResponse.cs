using CustomCADs.Catalog.Endpoints.Common.Dtos;

namespace CustomCADs.Catalog.Endpoints.Products.Contributors.Get.All;

public sealed record GetProductsResponse(
    Guid Id,
    string Name,
    string CreatorName,
    string UploadDate,
    ImageResponse Image,
    CategoryResponse Category
);
