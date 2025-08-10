using CustomCADs.Catalog.Domain.Products.ValueObjects;
using CustomCADs.Shared.Domain.Querying;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.GetAll;

public sealed record CreatorGetAllProductsQuery(
	Pagination Pagination,
	AccountId CreatorId,
	CategoryId? CategoryId = null,
	TagId[]? TagIds = null,
	string? Name = null,
	ProductSorting? Sorting = null
) : IQuery<Result<CreatorGetAllProductsDto>>;
