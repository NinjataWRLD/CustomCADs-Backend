namespace CustomCADs.Shared.UseCases.Users.Commands;

public record CreateUserCommand(
    string Role,
    string Username,
    string Email,
    string TimeZone,
    string? FirstName = default,
    string? LastName = default
) : ICommand<UserId>;
