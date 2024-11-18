namespace CustomCADs.Catalog.Endpoints.Products.GetAll;

public record GetProductsResponse(
    int Count,
    GetProductsDto[] Produts
);
