using CustomCADs.Catalog.Endpoints.Helpers.Dtos;

namespace CustomCADs.Catalog.Endpoints.Designer.Get.All;

public sealed record GetUncheckedProductsDto(
    Guid Id,
    string Name,
    string UploadDate,
    string CreatorName,
    ImageDto Image,
    CategoryResponse Category
);