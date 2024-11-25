using CustomCADs.Shared.Core.Common.TypedIds.Account;

namespace CustomCADs.Account.Application.Roles.Commands.DeleteById;

public record DeleteRoleByIdCommand(RoleId Id) : ICommand;
