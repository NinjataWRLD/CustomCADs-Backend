using CustomCADs.Shared.Core.Domain.ValueObjects.Ids;

namespace CustomCADs.Account.Application.Roles.Commands.Create;

public record CreateRoleCommand(RoleWriteDto Dto) : ICommand<RoleId>;
