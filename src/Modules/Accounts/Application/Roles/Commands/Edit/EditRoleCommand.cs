namespace CustomCADs.Accounts.Application.Roles.Commands.Edit;

public sealed record EditRoleCommand(
    string Name,
    RoleWriteDto Dto
) : ICommand;