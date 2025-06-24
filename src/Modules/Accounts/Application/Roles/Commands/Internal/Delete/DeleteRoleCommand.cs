namespace CustomCADs.Accounts.Application.Roles.Commands.Internal.Delete;

public sealed record DeleteRoleCommand(
	RoleId Id
) : ICommand;
