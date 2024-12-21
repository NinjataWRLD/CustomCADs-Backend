using CustomCADs.Catalog.Endpoints.Common.Dtos;

namespace CustomCADs.Catalog.Endpoints.Products.Designer.Get.All;

public sealed record GetUncheckedProductsDto(
    Guid Id,
    string Name,
    string UploadDate,
    string CreatorName,
    ImageResponse Image,
    CategoryResponse Category
);