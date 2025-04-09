namespace CustomCADs.Catalog.Endpoints.Products.Endpoints.Gallery.Get.All;

public sealed record GetAllGaleryProductsResponse(
    Guid Id,
    string Name,
    string[] Tags,
    string Category,
    int Views
);
