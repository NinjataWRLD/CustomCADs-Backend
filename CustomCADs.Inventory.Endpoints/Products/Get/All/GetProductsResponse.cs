using CustomCADs.Inventory.Endpoints.Helpers.Dtos;

namespace CustomCADs.Inventory.Endpoints.Products.Get.All;

public sealed record GetProductsResponse(
    Guid Id,
    string Name,
    string CreatorName,
    string UploadDate,
    ImageDto Image,
    CategoryResponse Category
);
