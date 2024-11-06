namespace CustomCADs.Account.Application.Roles.Commands.EditByName;

public record EditRoleByNameCommand(string Name, RoleWriteDto Dto) : ICommand;