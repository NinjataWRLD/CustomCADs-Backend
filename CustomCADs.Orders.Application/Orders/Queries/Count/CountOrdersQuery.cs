using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Orders.Application.Orders.Queries.Count;

public sealed record CountOrdersQuery(
    AccountId BuyerId
) : IQuery<CountOrdersDto>;
