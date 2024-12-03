using CustomCADs.Inventory.Endpoints.Helpers.Dtos;

namespace CustomCADs.Inventory.Endpoints.Products.Get.Recent;

public sealed record RecentProductsResponse(
    Guid Id,
    string Name,
    string Status,
    string UploadDate,
    CategoryResponse Category
);
