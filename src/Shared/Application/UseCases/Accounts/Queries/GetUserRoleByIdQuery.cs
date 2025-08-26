using CustomCADs.Shared.Application.Abstractions.Requests.Attributes;

namespace CustomCADs.Shared.Application.UseCases.Accounts.Queries;

[AddRequestCaching(ExpirationType.Absolute)]
public sealed record GetUserRoleByIdQuery(
	AccountId Id
) : IQuery<string>;
