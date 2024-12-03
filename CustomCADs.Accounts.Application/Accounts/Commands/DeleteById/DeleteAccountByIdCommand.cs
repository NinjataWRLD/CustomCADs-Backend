namespace CustomCADs.Accounts.Application.Accounts.Commands.DeleteById;

public sealed record DeleteAccountByIdCommand(
    AccountId Id
) : ICommand;