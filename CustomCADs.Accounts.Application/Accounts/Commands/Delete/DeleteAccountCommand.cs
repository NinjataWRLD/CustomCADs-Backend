namespace CustomCADs.Accounts.Application.Accounts.Commands.Delete;

public sealed record DeleteAccountCommand(
    string Username
) : ICommand;