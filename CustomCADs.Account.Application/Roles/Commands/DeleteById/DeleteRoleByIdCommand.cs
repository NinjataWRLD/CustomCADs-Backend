using CustomCADs.Account.Application.Common.Contracts;

namespace CustomCADs.Account.Application.Roles.Commands.DeleteById;

public record DeleteRoleByIdCommand(int Id) : ICommand;
