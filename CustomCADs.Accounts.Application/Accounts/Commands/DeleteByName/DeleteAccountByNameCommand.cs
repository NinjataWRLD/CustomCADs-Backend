namespace CustomCADs.Accounts.Application.Accounts.Commands.DeleteByName;

public record DeleteAccountByNameCommand(string Username) : ICommand;