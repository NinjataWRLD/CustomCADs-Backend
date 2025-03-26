using CustomCADs.Accounts.Application.Roles.Dtos;

namespace CustomCADs.Accounts.Application.Roles.Commands.Internal.Create;

public sealed record CreateRoleCommand(
    RoleWriteDto Dto
) : ICommand<RoleId>;
