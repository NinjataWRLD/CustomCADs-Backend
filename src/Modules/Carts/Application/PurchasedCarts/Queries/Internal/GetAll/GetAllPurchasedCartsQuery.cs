using CustomCADs.Carts.Domain.PurchasedCarts.Enums;
using CustomCADs.Carts.Domain.PurchasedCarts.ValueObjects;
using CustomCADs.Shared.Domain.Querying;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Carts.Application.PurchasedCarts.Queries.Internal.GetAll;

public sealed record GetAllPurchasedCartsQuery(
	Pagination Pagination,
	AccountId? BuyerId = null,
	PaymentStatus? PaymentStatus = null,
	PurchasedCartSorting? Sorting = null
) : IQuery<Result<GetAllPurchasedCartsDto>>;
