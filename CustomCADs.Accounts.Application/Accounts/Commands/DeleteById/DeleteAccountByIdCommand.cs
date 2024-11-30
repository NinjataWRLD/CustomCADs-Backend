namespace CustomCADs.Accounts.Application.Accounts.Commands.DeleteById;

public record DeleteAccountByIdCommand(AccountId Id) : ICommand;