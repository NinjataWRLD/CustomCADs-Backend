namespace CustomCADs.Catalog.Endpoints.Products.GetProducts;

public record GetProductsResponse(
    int Count, 
    GetProductsDto[] Produts
);
