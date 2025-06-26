namespace CustomCADs.Accounts.Application.Accounts.Commands.Internal.Delete;

public sealed record DeleteAccountCommand(
	AccountId Id
) : ICommand;
