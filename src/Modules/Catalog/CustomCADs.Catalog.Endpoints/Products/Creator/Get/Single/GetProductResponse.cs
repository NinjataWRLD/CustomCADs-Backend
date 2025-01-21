namespace CustomCADs.Catalog.Endpoints.Products.Creator.Get.Single;

public sealed record GetProductResponse(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    string UploadDate,
    CountsDto Counts,
    CategoryResponse Category
);
