using CustomCADs.Catalog.Endpoints.Categories;

namespace CustomCADs.Catalog.Endpoints.Products.RecentProducts;

public record RecentProductsResponse(
    ProductId Id,
    string Name,
    string Status,
    string UploadDate,
    CategoryResponse Category
);
