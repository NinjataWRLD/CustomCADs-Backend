using CustomCADs.Catalog.Domain.Products.Enums;
using CustomCADs.Catalog.Domain.Products.ValueObjects;
using CustomCADs.Shared.Domain.Querying;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Catalog.Application.Products.Queries.Internal.Designer.GetAll;

public sealed record DesignerGetAllProductsQuery(
	Pagination Pagination,
	AccountId DesignerId,
	ProductStatus Status,
	CategoryId? CategoryId = null,
	TagId[]? TagIds = null,
	string? Name = null,
	ProductSorting? Sorting = null
) : IQuery<Result<DesignerGetAllProductsDto>>;
