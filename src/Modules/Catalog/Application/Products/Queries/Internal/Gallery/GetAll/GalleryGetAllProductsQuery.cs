using CustomCADs.Catalog.Domain.Products.ValueObjects;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Catalog.Application.Products.Queries.Internal.Gallery.GetAll;

public sealed record GalleryGetAllProductsQuery(
	Pagination Pagination,
	AccountId BuyerId,
	CategoryId? CategoryId = null,
	TagId[]? TagIds = null,
	string? Name = null,
	ProductSorting? Sorting = null
) : IQuery<Result<GalleryGetAllProductsDto>>;
