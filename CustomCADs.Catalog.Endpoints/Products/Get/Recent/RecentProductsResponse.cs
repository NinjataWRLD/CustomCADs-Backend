namespace CustomCADs.Catalog.Endpoints.Products.Get.Recent;

public record RecentProductsResponse(
    Guid Id,
    string Name,
    string Status,
    string UploadDate,
    CategoryDto Category
);
