namespace CustomCADs.Accounts.Application.Roles.Commands.DeleteByName;

public record DeleteRoleByNameCommand(string Name) : ICommand;
