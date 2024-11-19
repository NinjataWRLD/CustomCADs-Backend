namespace CustomCADs.Catalog.Endpoints.Products.Get.All;

public record GetProductsResponse(
    int Count,
    GetProductsDto[] Produts
);
