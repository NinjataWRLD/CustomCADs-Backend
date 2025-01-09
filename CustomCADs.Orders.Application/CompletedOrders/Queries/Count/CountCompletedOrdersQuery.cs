using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Orders.Application.CompletedOrders.Queries.Count;

public sealed record CountCompletedOrdersQuery(
    AccountId BuyerId
) : IQuery<int>;
