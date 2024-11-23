using CustomCADs.Shared.Core.Dtos;

namespace CustomCADs.Inventory.Endpoints.Products.Get.All;

public record GetProductsDto(
    Guid Id,
    string Name,
    string CreatorName,
    string UploadDate,
    ImageDto Image,
    CategoryDto Category
);
