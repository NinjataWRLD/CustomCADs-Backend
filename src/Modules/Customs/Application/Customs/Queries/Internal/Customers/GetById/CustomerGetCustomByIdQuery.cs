using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Customs.Application.Customs.Queries.Internal.Customers.GetById;

public sealed record CustomerGetCustomByIdQuery(
    CustomId Id,
    AccountId BuyerId
) : IQuery<CustomerGetCustomByIdDto>;
