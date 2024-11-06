namespace CustomCADs.Account.Application.Users.Queries.GetAll;

public record GetAllUsersDto(int Count, ICollection<GetAllUsersItemDto> Users);

public record GetAllUsersItemDto(
    Guid Id,
    string Username,
    string Email,
    string Role,
    string? FirstName = null,
    string? LastName = null
);
