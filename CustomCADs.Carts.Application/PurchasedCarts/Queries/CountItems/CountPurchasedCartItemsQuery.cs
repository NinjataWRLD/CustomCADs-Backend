using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Carts.Application.PurchasedCarts.Queries.CountItems;

public sealed record CountPurchasedCartItemsQuery(
    AccountId BuyerId
) : IQuery<Dictionary<PurchasedCartId, int>>;
