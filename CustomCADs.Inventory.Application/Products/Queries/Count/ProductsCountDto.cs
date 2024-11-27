namespace CustomCADs.Inventory.Application.Products.Queries.Count;

public record ProductsCountDto(
    int Unchecked,
    int Validated,
    int Reported,
    int Banned
);