namespace CustomCADs.Catalog.Application.Products.Queries.GetAll;

public record GetAllProductsQuery(
    Guid? CreatorId = null,
    string? Status = null,
    string? Category = null,
    string? Name = null,
    string Sorting = "",
    int Page = 1,
    int Limit = 20);