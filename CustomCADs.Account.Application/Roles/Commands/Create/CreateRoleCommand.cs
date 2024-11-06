namespace CustomCADs.Account.Application.Roles.Commands.Create;

public record CreateRoleCommand(RoleWriteDto Dto) : ICommand<int>;
