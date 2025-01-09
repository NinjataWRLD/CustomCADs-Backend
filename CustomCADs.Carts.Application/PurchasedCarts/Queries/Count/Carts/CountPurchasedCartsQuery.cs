using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Carts.Application.PurchasedCarts.Queries.Count.Carts;

public sealed record CountPurchasedCartsQuery(
    AccountId BuyerId
) : IQuery<int>;
