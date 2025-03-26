using CustomCADs.Carts.Domain.PurchasedCarts.ValueObjects;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Carts.Application.PurchasedCarts.Queries.Internal.GetAll;

public sealed record GetAllPurchasedCartsQuery(
    Pagination Pagination,
    AccountId? BuyerId = null,
    PurchasedCartSorting? Sorting = null
) : IQuery<Result<GetAllPurchasedCartsDto>>;
