using CustomCADs.Catalog.Domain.Products.Enums;
using CustomCADs.Catalog.Domain.Products.ValueObjects;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Categories;

namespace CustomCADs.Catalog.Application.Products.Queries.GetAll;

public sealed record GetAllProductsQuery(
    CategoryId? CategoryId = null,
    AccountId? CreatorId = null,
    ProductStatus? Status = null,
    string? Name = null,
    ProductSorting? Sorting = null,
    int Page = 1,
    int Limit = 20
) : IQuery<Result<GetAllProductsDto>>;