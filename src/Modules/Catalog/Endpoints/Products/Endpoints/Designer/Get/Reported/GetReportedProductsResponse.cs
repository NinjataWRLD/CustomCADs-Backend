namespace CustomCADs.Catalog.Endpoints.Products.Endpoints.Designer.Get.Reported;

public sealed record GetReportedProductsResponse(
    Guid Id,
    string Name,
    string UploadedAt,
    string CreatorName,
    CategoryResponse Category
);