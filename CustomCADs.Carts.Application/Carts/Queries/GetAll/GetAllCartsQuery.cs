using CustomCADs.Carts.Domain.Carts.ValueObjects;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Carts.Application.Carts.Queries.GetAll;

public sealed record GetAllCartsQuery(
    Pagination Pagination,
    AccountId? BuyerId = null,
    CartSorting? Sorting = null
) : IQuery<Result<GetAllCartsDto>>;
