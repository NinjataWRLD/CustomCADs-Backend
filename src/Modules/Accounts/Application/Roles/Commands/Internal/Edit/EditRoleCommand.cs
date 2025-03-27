using CustomCADs.Accounts.Application.Roles.Dtos;

namespace CustomCADs.Accounts.Application.Roles.Commands.Internal.Edit;

public sealed record EditRoleCommand(
    string Name,
    RoleWriteDto Dto
) : ICommand;