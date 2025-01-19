using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Orders.Application.OngoingOrders.Queries.Count;

public sealed record CountOngoingOrdersQuery(
    AccountId BuyerId
) : IQuery<CountOngoingOrdersDto>;
