namespace CustomCADs.Accounts.Application.Roles.Commands.Internal.Create;

public sealed record CreateRoleCommand(
	RoleWriteDto Dto
) : ICommand<RoleId>;
