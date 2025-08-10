using CustomCADs.Carts.Domain.PurchasedCarts.Enums;
using CustomCADs.Carts.Domain.PurchasedCarts.ValueObjects;
using CustomCADs.Shared.Domain.Querying;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Carts.Domain.Repositories.Reads;

public record PurchasedCartQuery(
	Pagination Pagination,
	AccountId? BuyerId = null,
	PaymentStatus? PaymentStatus = null,
	PurchasedCartSorting? Sorting = null
);
