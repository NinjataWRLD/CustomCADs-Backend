namespace CustomCADs.Accounts.Application.Accounts.Commands.DeleteByName;

public sealed record DeleteAccountByNameCommand(
    string Username
) : ICommand;