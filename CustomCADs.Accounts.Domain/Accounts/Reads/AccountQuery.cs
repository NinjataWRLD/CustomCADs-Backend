using CustomCADs.Accounts.Domain.Accounts.ValueObjects;

namespace CustomCADs.Accounts.Domain.Accounts.Reads;

public record AccountQuery(
    AccountId[]? Ids = null,
    string? Role = null,
    string? Username = null,
    string? Email = null,
    string? FirstName = null,
    string? LastName = null,
    AccountSorting? Sorting = null,
    int Page = 1,
    int Limit = 20
);
