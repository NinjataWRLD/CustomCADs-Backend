using CustomCADs.Catalog.Endpoints.Products.Dtos;

namespace CustomCADs.Catalog.Endpoints.Products.Endpoints.Designer.Get.Reported;

public sealed record GetReportedProductsResponse(
    Guid Id,
    string Name,
    string UploadDate,
    string CreatorName,
    CategoryResponse Category
);