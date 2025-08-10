using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Carts.Application.PurchasedCarts.Queries.Internal.Count.Carts;

public sealed record CountPurchasedCartsQuery(
	AccountId BuyerId
) : IQuery<int>;
