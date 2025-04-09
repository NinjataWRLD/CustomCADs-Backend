namespace CustomCADs.Catalog.Endpoints.Products.Endpoints.Creator.Get.All;

public sealed record GetProductsResponse(
    Guid Id,
    string Name,
    DateTimeOffset UploadedAt,
    CategoryResponse Category
);
