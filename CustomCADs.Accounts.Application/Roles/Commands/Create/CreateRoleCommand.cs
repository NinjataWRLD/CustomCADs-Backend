namespace CustomCADs.Accounts.Application.Roles.Commands.Create;

public sealed record CreateRoleCommand(
    RoleWriteDto Dto
) : ICommand<RoleId>;
