namespace CustomCADs.Catalog.Endpoints.Products.Designer.Get.All;

public sealed record GetUncheckedProductsResponse(
    Guid Id,
    string Name,
    string UploadDate,
    string CreatorName,
    CategoryResponse Category
);