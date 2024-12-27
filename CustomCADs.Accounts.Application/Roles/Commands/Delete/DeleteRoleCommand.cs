namespace CustomCADs.Accounts.Application.Roles.Commands.Delete;

public sealed record DeleteRoleCommand(
    string Name
) : ICommand;
