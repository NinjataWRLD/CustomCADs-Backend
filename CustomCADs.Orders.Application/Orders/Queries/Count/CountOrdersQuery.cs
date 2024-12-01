using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Orders.Application.Orders.Queries.Count;

public record CountOrdersQuery(AccountId BuyerId) : IQuery<CountOrdersDto>;
