namespace CustomCADs.Accounts.Application.Roles.Commands.DeleteById;

public sealed record DeleteRoleByIdCommand(
    RoleId Id
) : ICommand;
