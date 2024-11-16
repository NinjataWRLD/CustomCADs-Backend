namespace CustomCADs.Account.Application.Users.Queries.GetAll;

public record GetAllUsersDto(int Count, ICollection<GetAllUsersItem> Users);

public record GetAllUsersItem(
    UserId Id,
    string Username,
    string Email,
    string Role,
    string? FirstName = null,
    string? LastName = null
);
