namespace CustomCADs.Account.Domain.Users.Reads;

public record UsersQuery(
    Guid[]? Ids = null,
    string? Role = null,
    string? Username = null,
    string? Email = null,
    string? FirstName = null,
    string? LastName = null,
    string Sorting = "",
    int Page = 1,
    int Limit = 20);
