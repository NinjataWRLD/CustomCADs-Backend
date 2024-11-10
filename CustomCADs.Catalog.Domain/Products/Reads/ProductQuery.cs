using CustomCADs.Catalog.Domain.Products.ValueObjects;

namespace CustomCADs.Catalog.Domain.Products.Reads;

public record ProductQuery(
    Guid? CreatorId = null,
    string? Status = null,
    string? Category = null,
    string? Name = null,
    ProductSorting? Sorting = null,
    int Page = 1,
    int Limit = 20
);