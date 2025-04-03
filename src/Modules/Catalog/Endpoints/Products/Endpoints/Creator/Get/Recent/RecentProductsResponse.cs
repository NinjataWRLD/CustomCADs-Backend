namespace CustomCADs.Catalog.Endpoints.Products.Endpoints.Creator.Get.Recent;

public sealed record RecentProductsResponse(
    Guid Id,
    string Name,
    string Status,
    string UploadedAt,
    CategoryResponse Category
);
