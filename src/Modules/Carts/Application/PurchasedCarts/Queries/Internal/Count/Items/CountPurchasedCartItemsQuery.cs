using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Carts.Application.PurchasedCarts.Queries.Internal.Count.Items;

public sealed record CountPurchasedCartItemsQuery(
	AccountId BuyerId
) : IQuery<Dictionary<PurchasedCartId, int>>;
