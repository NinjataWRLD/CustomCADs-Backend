namespace CustomCADs.Account.Application.Users.Commands.Create;

public record CreateUserCommand(
    string Role,
    string Username,
    string Email,
    string Password,
    string? FirstName,
    string? LastName
) : ICommand<Guid>;
