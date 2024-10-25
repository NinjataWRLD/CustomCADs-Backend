namespace CustomCADs.Account.Application.Users.Queries.GetAll;

public record GetAllUsersQuery(
    string? Role = null,
    string? Username = null,
    string? Email = null,
    string? FirstName = null,
    string? LastName = null,
    string Sorting = "",
    int Page = 1,
    int Limit = 20);