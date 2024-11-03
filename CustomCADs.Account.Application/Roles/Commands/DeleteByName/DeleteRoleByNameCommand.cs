using CustomCADs.Account.Application.Common.Contracts;

namespace CustomCADs.Account.Application.Roles.Commands.DeleteByName;

public record DeleteRoleByNameCommand(string Name) : ICommand;
