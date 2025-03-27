using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Orders.Application.CompletedOrders.Queries.Internal.Count;

public sealed record CountCompletedOrdersQuery(
    AccountId BuyerId
) : IQuery<int>;
