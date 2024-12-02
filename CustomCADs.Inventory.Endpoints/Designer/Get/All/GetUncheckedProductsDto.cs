using CustomCADs.Inventory.Endpoints.Helpers.Dtos;

namespace CustomCADs.Inventory.Endpoints.Designer.Get.All;

public record GetUncheckedProductsDto(
    Guid Id,
    string Name,
    string UploadDate,
    string CreatorName,
    ImageDto Image,
    CategoryResponse Category
);