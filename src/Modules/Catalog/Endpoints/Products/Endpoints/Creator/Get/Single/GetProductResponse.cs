using CustomCADs.Catalog.Endpoints.Products.Dtos;

namespace CustomCADs.Catalog.Endpoints.Products.Endpoints.Creator.Get.Single;

public sealed record GetProductResponse(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    string UploadedAt,
    CountsDto Counts,
    CategoryResponse Category
);
