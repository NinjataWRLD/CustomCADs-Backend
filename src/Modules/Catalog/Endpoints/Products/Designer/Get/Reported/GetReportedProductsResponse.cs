namespace CustomCADs.Catalog.Endpoints.Products.Designer.Get.Reported;

public sealed record GetReportedProductsResponse(
    Guid Id,
    string Name,
    string UploadDate,
    string CreatorName,
    CategoryResponse Category
);