using CustomCADs.Accounts.Domain.Accounts.ValueObjects;
using CustomCADs.Shared.Core.Common;

namespace CustomCADs.Accounts.Domain.Accounts.Reads;

public record AccountQuery(
    Pagination Pagination,
    AccountId[]? Ids = null,
    string? Role = null,
    string? Username = null,
    string? Email = null,
    string? FirstName = null,
    string? LastName = null,
    AccountSorting? Sorting = null
);
