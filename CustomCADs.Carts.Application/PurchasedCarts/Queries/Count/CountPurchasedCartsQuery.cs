using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Carts.Application.PurchasedCarts.Queries.Count;

public sealed record CountPurchasedCartsQuery(
    AccountId BuyerId
) : IQuery<int>;
