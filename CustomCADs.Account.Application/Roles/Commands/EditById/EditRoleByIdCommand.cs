using CustomCADs.Shared.Core.Common.TypedIds.Account;

namespace CustomCADs.Account.Application.Roles.Commands.EditById;

public record EditRoleByIdCommand(RoleId Id, RoleWriteDto Dto) : ICommand;