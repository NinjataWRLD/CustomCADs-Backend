namespace CustomCADs.Catalog.Presentation.Products.Endpoints.CountProducts;

public record CountProductsResponse(
        int Unchecked,
        int Validated,
        int Reported,
        int Banned
    );
