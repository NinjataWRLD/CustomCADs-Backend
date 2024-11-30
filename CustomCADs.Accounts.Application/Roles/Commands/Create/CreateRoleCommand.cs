using CustomCADs.Accounts.Application.Roles.Commands;

namespace CustomCADs.Accounts.Application.Roles.Commands.Create;

public record CreateRoleCommand(RoleWriteDto Dto) : ICommand<RoleId>;
