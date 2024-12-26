namespace CustomCADs.Catalog.Endpoints.Products.Contributors.Get.Recent;

public sealed record RecentProductsResponse(
    Guid Id,
    string Name,
    string Status,
    string UploadDate,
    CategoryResponse Category
);
