namespace CustomCADs.Shared.UseCases.Accounts.Commands;

public record CreateAccountCommand(
    string Role,
    string Username,
    string Email,
    string TimeZone,
    string? FirstName = default,
    string? LastName = default
) : ICommand<AccountId>;
