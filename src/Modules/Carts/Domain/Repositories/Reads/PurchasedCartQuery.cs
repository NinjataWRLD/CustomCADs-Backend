using CustomCADs.Carts.Domain.PurchasedCarts.ValueObjects;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Carts.Domain.Repositories.Reads;

public record PurchasedCartQuery(
	Pagination Pagination,
	AccountId? BuyerId = null,
	PurchasedCartSorting? Sorting = null
);
