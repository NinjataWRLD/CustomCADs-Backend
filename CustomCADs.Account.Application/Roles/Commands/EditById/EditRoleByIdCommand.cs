namespace CustomCADs.Account.Application.Roles.Commands.EditById;

public record EditRoleByIdCommand(RoleId Id, RoleWriteDto Dto) : ICommand;