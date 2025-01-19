namespace CustomCADs.Catalog.Application.Products.Queries.Count;

public record ProductsCountDto(
    int Unchecked,
    int Validated,
    int Reported,
    int Banned
);