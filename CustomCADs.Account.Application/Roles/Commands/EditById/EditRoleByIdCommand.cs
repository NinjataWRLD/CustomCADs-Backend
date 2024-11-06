namespace CustomCADs.Account.Application.Roles.Commands.EditById;

public record EditRoleByIdCommand(int Id, RoleWriteDto Dto) : ICommand;