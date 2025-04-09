using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Customs.Application.Customs.Queries.Internal.Customers.Count;

public sealed record CountCustomsQuery(
    AccountId BuyerId
) : IQuery<CountCustomsDto>;
