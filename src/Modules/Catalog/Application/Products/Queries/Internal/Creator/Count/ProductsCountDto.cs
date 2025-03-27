namespace CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.Count;

public record ProductsCountDto(
    int Unchecked,
    int Validated,
    int Reported,
    int Banned
);