using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Orders.Application.OngoingOrders.Queries.Internal.Count;

public sealed record CountOngoingOrdersQuery(
    AccountId BuyerId
) : IQuery<CountOngoingOrdersDto>;
