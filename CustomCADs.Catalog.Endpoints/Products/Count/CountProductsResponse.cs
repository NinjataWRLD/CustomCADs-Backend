namespace CustomCADs.Catalog.Endpoints.Products.Count;

public record CountProductsResponse(
    int Unchecked,
    int Validated,
    int Reported,
    int Banned
);
