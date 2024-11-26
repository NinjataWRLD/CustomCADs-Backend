using CustomCADs.Account.Domain.Users.ValueObjects;

namespace CustomCADs.Account.Domain.Users.Reads;

public record UserQuery(
    UserId[]? Ids = null,
    string? Role = null,
    string? Username = null,
    string? Email = null,
    string? FirstName = null,
    string? LastName = null,
    UserSorting? Sorting = null,
    int Page = 1,
    int Limit = 20
);
