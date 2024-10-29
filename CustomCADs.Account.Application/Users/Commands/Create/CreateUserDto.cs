namespace CustomCADs.Account.Application.Users.Commands.Create;

public record CreateUserDto(string RoleName, string Username, string Email, string? FirstName, string? LastName);
