namespace CustomCADs.Catalog.Endpoints.Products.Designer.Get.Single;

public sealed record DesignerSingleProductResponse(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    string CreatorName,
    CategoryResponse Category
);
