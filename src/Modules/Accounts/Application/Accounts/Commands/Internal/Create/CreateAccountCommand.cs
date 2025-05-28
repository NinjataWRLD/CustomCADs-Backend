namespace CustomCADs.Accounts.Application.Accounts.Commands.Internal.Create;

public sealed record CreateAccountCommand(
	string Role,
	string Username,
	string Email,
	string Password,
	string? FirstName,
	string? LastName
) : ICommand<AccountId>;
