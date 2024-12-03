namespace CustomCADs.Accounts.Application.Roles.Commands.EditById;

public sealed record EditRoleByIdCommand(
    RoleId Id,
    RoleWriteDto Dto
) : ICommand;