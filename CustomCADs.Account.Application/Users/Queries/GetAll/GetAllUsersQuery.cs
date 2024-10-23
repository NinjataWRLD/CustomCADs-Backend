namespace CustomCADs.Account.Application.Users.Queries.GetAll;

public record GetAllUsersQuery(
    bool? HasRT = null,
    string? Username = null,
    string? Email = null,
    string? FirstName = null,
    string? LastName = null,
    DateTime? RtEndDateBefore = null,
    DateTime? RtEndDateAfter = null,
    string Sorting = "",
    int Page = 1,
    int Limit = 20);