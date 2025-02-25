namespace CustomCADs.Catalog.Endpoints.Products.Designer.Get.Unchecked;

public sealed record GetUncheckedProductsResponse(
    Guid Id,
    string Name,
    string UploadDate,
    string CreatorName,
    CategoryResponse Category
);