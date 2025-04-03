using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Carts.Application.ActiveCarts.Queries.Internal.Count;

public sealed record CountActiveCartItemsQuery(
    AccountId BuyerId
) : IQuery<int>;
