namespace CustomCADs.Accounts.Application.Roles.Commands.EditByName;

public sealed record EditRoleByNameCommand(
    string Name,
    RoleWriteDto Dto
) : ICommand;