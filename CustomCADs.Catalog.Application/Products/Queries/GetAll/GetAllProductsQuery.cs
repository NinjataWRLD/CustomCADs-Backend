using CustomCADs.Catalog.Domain.Products.ValueObjects;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Account;

namespace CustomCADs.Catalog.Application.Products.Queries.GetAll;

public record GetAllProductsQuery(
    UserId? CreatorId = null,
    string? Status = null,
    string? Name = null,
    ProductSorting? Sorting = null,
    int Page = 1,
    int Limit = 20
) : IQuery<GetAllProductsDto>;