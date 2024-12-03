using CustomCADs.Accounts.Domain.Accounts.ValueObjects;
using CustomCADs.Shared.Core.Common;

namespace CustomCADs.Accounts.Application.Accounts.Queries.GetAll;

public sealed record GetAllAccountsQuery(
    string? Role = null,
    string? Username = null,
    string? Email = null,
    string? FirstName = null,
    string? LastName = null,
    AccountSorting? Sorting = null,
    int Page = 1,
    int Limit = 20
) : IQuery<Result<GetAllAccountsItem>>;