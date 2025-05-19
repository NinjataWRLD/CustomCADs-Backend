namespace CustomCADs.Shared.UseCases.Accounts.Commands;

public sealed record CreateAccountCommand(
	string Role,
	string Username,
	string Email,
	string? FirstName = default,
	string? LastName = default
) : ICommand<AccountId>;
