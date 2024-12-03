using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Carts.Application.Carts.Queries.CountItems;

public sealed record CountCartItemsQuery(
    AccountId BuyerId
) : IQuery<Dictionary<CartId, int>>;
