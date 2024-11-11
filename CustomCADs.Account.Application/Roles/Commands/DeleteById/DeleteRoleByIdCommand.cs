using CustomCADs.Shared.Core.Domain.ValueObjects.Ids;

namespace CustomCADs.Account.Application.Roles.Commands.DeleteById;

public record DeleteRoleByIdCommand(RoleId Id) : ICommand;
