namespace CustomCADs.Catalog.Endpoints.Products.GetProducts;

public record GetProductsRequest(string? Sorting = default, string? Category = default, string? Name = default, int Page = 1, int Limit = 20);
