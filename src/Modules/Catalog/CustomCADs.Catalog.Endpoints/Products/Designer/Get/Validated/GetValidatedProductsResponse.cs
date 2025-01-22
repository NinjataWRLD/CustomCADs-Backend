namespace CustomCADs.Catalog.Endpoints.Products.Designer.Get.Validated;

public sealed record GetValidatedProductsResponse(
    Guid Id,
    string Name,
    string UploadDate,
    string CreatorName,
    CategoryResponse Category
);