namespace CustomCADs.Catalog.Endpoints.Products.Contributors.Get.Single;

public sealed record GetProductResponse(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    string UploadDate,
    Guid CadId,
    CountsDto Counts,
    CategoryResponse Category
);
