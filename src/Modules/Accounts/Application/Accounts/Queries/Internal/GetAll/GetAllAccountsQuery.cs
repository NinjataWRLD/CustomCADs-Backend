using CustomCADs.Accounts.Domain.Accounts.ValueObjects;
using CustomCADs.Shared.Domain.Querying;

namespace CustomCADs.Accounts.Application.Accounts.Queries.Internal.GetAll;

public sealed record GetAllAccountsQuery(
	Pagination Pagination,
	string? Role = null,
	string? Username = null,
	string? Email = null,
	string? FirstName = null,
	string? LastName = null,
	AccountSorting? Sorting = null
) : IQuery<Result<GetAllAccountsDto>>;
