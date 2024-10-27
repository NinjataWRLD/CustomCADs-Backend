namespace CustomCADs.Catalog.Endpoints.Products.Endpoints.CountProducts;

public record CountProductsResponse(
        int Unchecked,
        int Validated,
        int Reported,
        int Banned
    );
