namespace CustomCADs.Catalog.Endpoints.Products.CountProducts;

public record CountProductsResponse(
        int Unchecked,
        int Validated,
        int Reported,
        int Banned
    );
