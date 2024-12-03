namespace CustomCADs.Accounts.Application.Roles.Commands.DeleteByName;

public sealed record DeleteRoleByNameCommand(
    string Name
) : ICommand;
