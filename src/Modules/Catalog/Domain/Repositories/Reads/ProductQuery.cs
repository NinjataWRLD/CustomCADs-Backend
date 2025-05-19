using CustomCADs.Catalog.Domain.Products.Enums;
using CustomCADs.Catalog.Domain.Products.ValueObjects;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Categories;

namespace CustomCADs.Catalog.Domain.Repositories.Reads;

public record ProductQuery(
	Pagination Pagination,
	ProductId[]? Ids = null,
	TagId[]? TagIds = null,
	AccountId? DesignerId = null,
	AccountId? CreatorId = null,
	CategoryId? CategoryId = null,
	ProductStatus? Status = null,
	string? Name = null,
	ProductSorting? Sorting = null
);
