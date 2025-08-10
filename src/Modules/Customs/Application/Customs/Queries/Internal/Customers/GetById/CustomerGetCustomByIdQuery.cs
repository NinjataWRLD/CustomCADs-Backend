using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Customs.Application.Customs.Queries.Internal.Customers.GetById;

public sealed record CustomerGetCustomByIdQuery(
	CustomId Id,
	AccountId BuyerId
) : IQuery<CustomerGetCustomByIdDto>;
