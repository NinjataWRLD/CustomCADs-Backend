namespace CustomCADs.Catalog.Domain.Products.Reads;

public record ProductQuery(
    Guid? CreatorId = null,
    string? Status = null,
    string? Category = null,
    string? Name = null,
    string Sorting = "",
    int Page = 1,
    int Limit = 20);